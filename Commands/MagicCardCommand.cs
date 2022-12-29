using DiscordAyta.Models;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DiscordAyta.Commands
{
    public class MagicCardCommand : BaseCommandModule
    {
        private JsonSerializerOptions serializerOptions = new() { PropertyNameCaseInsensitive = true };

        /// <summary>
        /// Send a list of messages sequentially to the channel where a MessageCreateEventArgs
        /// event was triggered
        /// </summary>
        /// <param name="messageList">Messages to send</param>
        /// <param name="e">The MessageCreateEventArgs event that the messages are sent in response to</param>
        /// <returns>Asynchronous operation</returns>
        private static async Task SendMessages(IEnumerable<string> messageList, MessageCreateEventArgs e)
        {
            foreach (string message in messageList)
            {
                await e.Channel.SendMessageAsync(message);
            }
        }

        /// <summary>
        /// Send a list of messages sequentially to the channel where a MessageReactionAddEventArgs
        /// event was triggered
        /// </summary>
        /// <param name="messageList">Messages to send</param>
        /// <param name="e">The MessageReactionAddEventArgs event that the messages are sent in response to</param>
        /// <returns>Asynchronous operation</returns>
        private static async Task SendMessages(IEnumerable<string> messageList, MessageReactionAddEventArgs e)
        {
            foreach (string message in messageList)
            {
                await e.Channel.SendMessageAsync(message);
            }
        }
        
        /// <summary>
        /// Submit a request to the given address and attempt to parse the JSON response
        /// into either a Card or Error object
        /// </summary>
        /// <param name="requestUri">The REST endpoint to access</param>
        /// <returns>A MagicModel object which is likely either a Card or Error</returns>
        public async Task<MagicModel> GetMagicModel(string requestUri)
        {
            var response = await SubmitRequest(requestUri);

            // Read the resulting content of the submitted request
            string jsonString = response.Content.ReadAsStringAsync().Result;

            // Attempt to convert the jsonString to a MagicModel object
            MagicModel magicModel = JsonSerializer.Deserialize<MagicModel>(jsonString, serializerOptions);

            // Check if the MagicModel object can be converted to a Card or Error object
            if (magicModel.Object == "card")
            {
                // Convert the MagicModel object to a Card and return it
                Card card = JsonSerializer.Deserialize<Card>(jsonString, serializerOptions);

                return card;
            }
            else if (magicModel.Object == "error")
            {
                // Convert the MagicModel object to an Error and return it
                Error error = JsonSerializer.Deserialize<Error>(jsonString, serializerOptions);

                return error;
            }

            return magicModel;
        }

        /// <summary>
        /// Attempt to get a Card image based on a message sent over Discord
        /// </summary>
        /// <param name="s">Standard Discord API wrapper</param>
        /// <param name="e">MessageCreateEventArgs that triggered this method call</param>
        /// <returns>Asynchronous operation</returns>
        public async Task GetCardImage(DiscordClient s, MessageCreateEventArgs e)
        {
            // Initialize output lists
            List<string> imageUris = new();
            List<string> imageFailures = new();

            // Attempt to parse and process card names from the message content
            List<string> cardNames = GetCardNames(e.Message.Content);
            List<string> processedCardNames = ProcessCardNames(cardNames);

            foreach (string name in processedCardNames)
            {
                // Create the URI to be used for our request
                string requestUri = $"https://api.scryfall.com/cards/named?fuzzy={name}";
                MagicModel modelObject = GetMagicModel(requestUri).Result;

                // Check if the MagicModel object we got back is a Card or Error object
                if (modelObject.Object == "card")
                {
                    // Convert the MagicModel object once again into a Card
                    Card cardObject = (Card)modelObject;

                    // Gather the high-quality PNG URI from our parsed Card object
                    string imageUri = cardObject.ImageUris["png"].OriginalString;

                    imageUris.Add(imageUri);
                }
                else if(modelObject.Object == "error")
                {
                    // Convert the Magic Model object once again into an Error
                    Error errorObject = (Error)modelObject;

                    // Gather the specific failure details of the parsed Error object
                    string imageFailure = errorObject.Details;

                    imageFailures.Add(imageFailure);
                }
            }

            // Add our failure messages onto our successfully obtained image URIs
            IEnumerable<string> imageMessages = imageUris.Concat(imageFailures);

            // Send all messages sequentially
            await SendMessages(imageMessages, e);
        }

        /// <summary>
        /// Attempt to get Card Rulings based on a reaction to a message
        /// </summary>
        /// <param name="s">Standard Discord API wrapper</param>
        /// <param name="e">MessageReactionAddEventArgs that triggered this method call</param>
        /// <returns>Asynchronous operation</returns>
        public async Task GetCardRulings(DiscordClient s, MessageReactionAddEventArgs e)
        {
            // Parse the Card ID based on the message content
            string parsedCardId = ParseCardIdFromImageUri(e.Message.Content);

            // Create the URI to be used for our request
            string requestUri = $"https://api.scryfall.com/cards/{parsedCardId}";

            MagicModel modelObject = GetMagicModel(requestUri).Result;

            // Check if the MagicModel object we got back is a Card object
            if (modelObject.Object == "card")
            {
                // Convert the MagicModel object once again into a Card object
                Card cardObject = (Card)modelObject;

                // Gather the Rulings URI from our parsed Card Object
                var rulingsUri = cardObject.RulingsUri.OriginalString;

                // Submit and read the resulting content of the submitted request
                var rulingsResponse = SubmitRequest(rulingsUri);
                var rulingsResult = rulingsResponse.Result.Content.ReadAsStringAsync().Result;

                // Convert the gathered list of Rulings to a ResultsList object
                var resultsList = JsonSerializer.Deserialize<ResultsList>(rulingsResult, serializerOptions);

                // Initialize output list
                List<string> rulingsList = new();

                foreach (JsonElement results in resultsList.Data)
                {
                    // Convert each JsonElement in the ResultsList object to a Rulings object
                    Rulings rulings = results.Deserialize<Rulings>(serializerOptions);

                    // Boldly enumerate, then add the Rulings comment to our output list
                    rulingsList.Add($"**{rulingsList.Count + 1}.** {rulings.Comment}");
                }

                // Initialize acceptable Emoji reactions
                var questionEmoji = DiscordEmoji.FromName(s, ":question:");
                var greyQuestionEmoji = DiscordEmoji.FromName(s, ":grey_question:");

                // Only send our output messages if the message has one of our accepted Emojis
                if (e.Emoji == questionEmoji || e.Emoji == greyQuestionEmoji)
                {
                    if (rulingsList.Count > 0)
                    {
                        // If we have at least one Ruling, make our initial message the name of the Card (in bold)
                        rulingsList.Insert(0, $"**{cardObject.Name}**");
                    }
                    else
                    {
                        // If there are no Rulings, give a message indicating this fact
                        rulingsList.Add($"**{cardObject.Name}**\n0. No additional rulings.");
                    }

                    await SendMessages(rulingsList, e);
                }
            }
        }
        
        /// <summary>
        /// Attempt to find any possible Card names in a Discord message
        /// </summary>
        /// <param name="text">Message sent over Discord</param>
        /// <returns>A List of possible Card names based on our search parameters</returns>
        private static List<string> GetCardNames(string text)
        {
            // Initialize list of Cards we will look for
            List<string> cardNames = new();

            // Make our pattern [[...]], which will be used to look for any text between two sets of square brackets
            string pattern = string.Format(
                "{0}({1}){2}",
                Regex.Escape("[["),
                ".+?",
                Regex.Escape("]]"));

            // Use our pattern to find any matches of our pattern
            foreach (Match m in Regex.Matches(text, pattern))
            {
                // Add only the (possible) Card name to our output, not "[[" or "]]"
                cardNames.Add(m.Groups[1].Value);
            }

            return cardNames;
        }

        /// <summary>
        /// Convert a List of given Card names to be compatible with URIs
        /// </summary>
        /// <param name="cardNames"></param>
        /// <returns>A List of processed Card names</returns>
        private static List<string> ProcessCardNames(List<string> cardNames)
        {
            // Initialize our list of processed Card names
            List<string> updatedCardNames = new();

            for (int i = 0; i < cardNames.Count; i++)
            {
                // Remove any leading or trailing whitespace
                // Convert any group of spaces in the Card name to a single "+" character
                // Make the card name lowercase (not required)
                string processedCardName = Regex.Replace(cardNames[i].Trim(), @"\s+", "+").ToLower();

                updatedCardNames.Add(processedCardName);
            }

            return cardNames;
        }

        /// <summary>
        /// Extract the Card Id from an image URI, which was likely (but not necessarily)
        /// sent by the bot through a previous GetCardImage call
        /// </summary>
        /// <param name="imageUri">The URI of a Card image</param>
        /// <returns></returns>
        private string ParseCardIdFromImageUri(string imageUri)
        {
            // ---- Samples -----
            // imageUri: https://cards.scryfall.io/large/front/b/d/bd8fa327-dd41-4737-8f19-2cf5eb1f7cdd.jpg?1614638838
            // cardUri:  https://api.scryfall.com/cards/bd8fa327-dd41-4737-8f19-2cf5eb1f7cdd

            // Make our pattern ________-____-____-____-____________,
            // Where each underscore represents a hexidemical value, overall forming our Card Id.
            // This pattern can be seen in the above samples, "bd8fa327-dd41-4737-8f19-2cf5eb1f7cdd"
            string pattern = string.Format("\\w{{8}}-\\w{{4}}-\\w{{4}}-\\w{{4}}-\\w{{12}}");

            // Extract the aforementioned Card Id
            var match = Regex.Match(imageUri, pattern);

            // Convert the Card Id from a Match object to a string for our return
            string cardUri = match.ToString();

            return cardUri;
        }

        /// <summary>
        /// Send a GET request to the Scryfall API
        /// </summary>
        /// <param name="requestUri">The REST endpoint of our request</param>
        /// <returns>An HTTP response message for our submitted request</returns>
        private static async Task<HttpResponseMessage> SubmitRequest(string requestUri)
        {
            // Delay each request by 50 milliseconds to avoid overloading the Scryfall API
            await Task.Delay(50);

            // Create an HttpClient and submit our GET request through it
            HttpClient client = new();
            var response = await client.GetAsync(requestUri);

            return response;
        }
    }
}

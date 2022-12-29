using DSharpPlus.SlashCommands;

namespace DiscordAyta.Commands
{
    public class GreetCommand : ApplicationCommandModule
    {
        [SlashCommand("greet", "Gives a greeting")]
        public async Task Greet(InteractionContext ctx)
        {
            // Initialize random object
            var rand = new Random();

            // Establish list of responses
            List<string> greetings = new()
            {
                "hi",
                "hello",
                "howdy",
                "hey",
                "salutations",
                "how do you do?",
                "greetings",
                "hi there",
                "hey there",
                "hello there",
                "good to see you",
                "you make me sick",
                "how's it going?",
                "how are things?",
                "what's up"
            };

            // Randomly select one of our established greetings
            string selection = greetings[rand.Next(greetings.Count)];

            await ctx.CreateResponseAsync(selection);
        }
    }
}

using DiscordAyta.Commands;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using DiscordAyta.Resources;

namespace DiscordAyta
{
    class Program
    {
        public static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        public static async Task MainAsync()
        {
            // Set up bot with permissions
            var discordConfig = new DiscordConfiguration()
            {
                Token = GetToken(),
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged
            };

            // Initialize our DiscordClient with our established bot config
            var discordClient = new DiscordClient(discordConfig);
            
            // Establish message triggers
            discordClient.MessageCreated += new MagicCardCommand().GetCardImage;
            discordClient.MessageReactionAdded += new MagicCardCommand().GetCardRulings;
            
            // Use and register Slash Commands
            var slashCmds = discordClient.UseSlashCommands();
            slashCmds.RegisterCommands<GreetCommand>();
            slashCmds.RegisterCommands<RollCommand>();
            slashCmds.RegisterCommands<PingCommand>();

            // Authenticate and connect our bot
            await discordClient.ConnectAsync();
            
            // Keep our task running indefinitely
            await Task.Delay(-1);
        }

        /// <summary>
        /// Rudimentary Token reading method, just calls the static GetTokenValue method
        /// </summary>
        /// <returns>Token value</returns>
        private static string GetToken()
        {
            string tokenValue = Token.GetTokenValue();

            return tokenValue;
        }
    }
}
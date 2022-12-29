using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System.Diagnostics;

namespace DiscordAyta.Commands
{
    public class TurnCommands : ApplicationCommandModule
    {
        Dictionary<DiscordChannel, List<string>> TurnOrders = new Dictionary<DiscordChannel, List<string>>();

        [SlashCommand("turn", "Signal that your turn is over")]
        public async Task Turn(InteractionContext ctx)
        {
            DiscordChannel channel = ctx.Channel;
            string lastPlayer = ctx.User.Username;

            List<string> players = TurnOrders[channel];
            Debug.WriteLine("Succeeded players = TurnOrders[channel] step");

            string nextPlayer = players[(players.IndexOf(lastPlayer) + 1) % players.Count];
            
            Debug.WriteLine("Succeeded nextPlayer step");
            
            await ctx.CreateResponseAsync($"@{nextPlayer}, it's your turn");
        }

        [SlashCommand("turnOrder", "Establish turn order")]
        public async Task TurnOrder(InteractionContext ctx, [Option("usernames", "Usernames of players in turn order, comma-delimited")] string usernames)
        {
            List<string> usernameList = usernames.Replace(" ", "").Split(",").ToList();

            DiscordChannel channel = ctx.Channel;
            TurnOrders.Add(channel, usernameList);

            await ctx.CreateResponseAsync($"Successfully created {usernames} turn order in #{channel.Name}");
        }

        [SlashCommand("resetTurnOrder", "Reset turn order so it can be set again")]
        public void ResetTurnOrder(InteractionContext ctx)
        {
            DiscordChannel channel = ctx.Channel;

            TurnOrders.Remove(channel);
        }
    }
}

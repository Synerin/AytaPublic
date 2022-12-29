using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace DiscordAyta.Commands
{
    public class PingCommand : ApplicationCommandModule
    {
        [SlashCommand("ping", "Returns command latency")]
        public async Task Ping(InteractionContext ctx)
        {
            // Create a Discord ping pong emoji
            var emoji = DiscordEmoji.FromName(ctx.Client, ":ping_pong:");

            // Respond with emoji and indication of client latency
            await ctx.CreateResponseAsync($"{emoji} Ping is {ctx.Client.Ping} ms");
        }
    }
}

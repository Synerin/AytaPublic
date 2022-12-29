using DSharpPlus.SlashCommands;

namespace DiscordAyta.Commands
{
    public class RollCommand : ApplicationCommandModule
    {
        [SlashCommand("roll", "Rolls up to 10 n-sided dice, n <= 100")]
        public async Task Roll(InteractionContext ctx, [Option("dice", "Dice to roll")] string input)
        {
            int dice = 0;
            int sides = 0;

            try
            {
                // Parse dice and sides from given input
                string[] values = input.Split('d');

                dice = int.Parse(values[0]);
                sides = int.Parse(values[1]);

                // Throw exceptions if too few/many dice or sides are requested
                if (dice < 1 || 10 < dice) throw new Exception("Invalid dice count");
                if (sides < 1 || 100 < sides) throw new Exception("Invalid side count");
            }
            catch
            {
                // Send message indicating proper formatting, will eventually disappear
                await ctx.CreateResponseAsync("Invalid input. Input must be in the format of xdn, where x = [1, 10] and n = [2, 100]", ephemeral: true);
            }

            var rand = new Random();

            // Initialize output array
            int[] rolls = new int[dice];

            // Get a random die value given the number of sides on the die
            for (int i = 0; i < rolls.Length; i++)
            {
                // Increment result as it will be a value [0, sides)
                // (The result will be from 0 upto (but not including) the # of sides
                rolls[i] = 1 + rand.Next(sides);
            }

            // Initialize output as string
            string output = "";

            for (int i = 0; i < rolls.Length; i++)
            {
                string rollStr = rolls[i].ToString();

                // Bolden lowest possible rolls and underline highest possible rolls
                if (rolls[i] == 1)
                {
                    output += "**" + rollStr + "**";
                }
                else if (rolls[i] == sides)
                {
                    output += "__" + rollStr + "__";
                }
                else
                {
                    output += rollStr;
                }

                // Denote addition if multiple dice are being rolled
                if (i < rolls.Length - 1) output += " + ";
            }

            // Denote and calculate summation if multiple dice are being rolled
            if(dice > 1) output += " = " + rolls.Sum();

            // Format output to be two lines below indication of input
            await ctx.CreateResponseAsync($"Rolling {input}\n\n{output}");
        }
    }
}

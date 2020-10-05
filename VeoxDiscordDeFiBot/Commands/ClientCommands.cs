using Dapper;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeoxDiscordDeFiBot.Commands
{
    class ClientCommands : BaseCommandModule
    {
        private readonly IConnectionFactory connectionFactory;

        public ClientCommands()
        {
            this.connectionFactory = new SqlServerConnectionFactory(@"Database=LeechBA;Data Source=(LocalDb)\VitaCafe;Integrated Security=SSPI;");
        }


        [Command("stats"), Aliases("s", "v")]
        public async Task Stats(CommandContext ctx, string id)
        {
            if(id.Length > 10 )
            {
                var token = await TheGraph.GetTokenByID(id);
                var embed = DiscordEmbed.TokenStatsEmbed(token);
                await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
            }
            else
            {
                var tokens = await TheGraph.GetTokensBySymbol(id);
                if (tokens.Count == 0)
                    return;
                var embeds = DiscordEmbed.TokenStatsEmbeds(tokens);
                foreach(var embed in embeds)
                    await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
            }
            //if(SymbolIDPairs.pairs.ContainsKey(id.ToUpper()))
            //    id = SymbolIDPairs.pairs[id.ToUpper()];

            
        }

        [Command("allstats")]
        public async Task AllStats(CommandContext ctx, string id = "x")
        {
            if (SymbolIDPairs.pairs.ContainsKey(id.ToUpper()))
                id = SymbolIDPairs.pairs[id.ToUpper()];

            var token = await TheGraph.GetTokenByID(id);
            var embed = DiscordEmbed.TokenStatsAllEmbed(token);
            await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
        }
        [Command("veoxinformation")]
        public async Task Test(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync($"Welcome @everyone to the " +
                $"{DiscordEmoji.FromName(ctx.Client, ":Veox:")} " +
                $"{DiscordEmoji.FromName(ctx.Client, ":VeoxE:")} " +
                $"{DiscordEmoji.FromName(ctx.Client, ":VeoxO:")} " +
                $"{DiscordEmoji.FromName(ctx.Client, ":VeoxX:")}" +
                $" community!\n\nWe are currently working our asses off to provide you with " +
                "the best DeFi Discord BOT of 2020, and we will deliver 💯\n\nIf you want to add the bot to your server just tag us!" +
                "\n\nWe are also in the process of developing a token that with a 🔥 REVOLUTIONARY 🔥 stake contract (prepare for legendary charts and profits 💸)!" +
                "\nIf you have any questions ask away!" +
                "\n\nWith lots of love 😘❤️\n" +
                "- Team VEOX").ConfigureAwait(false);
        }
        [Command("help")]
        public async Task Help(CommandContext ctx)
        {
            var pricesEmbed = new DiscordEmbedBuilder
            {
                Title = getEmoji("gear") + " Commands " + getEmoji("gear"),
                Color = DiscordColor.PhthaloGreen,
            };

            pricesEmbed.AddField("!help", "Displays the list of available commands.", false);
            pricesEmbed.AddField("!s {TOKEN_SYMBOL}", "Displays data of token(s) corresponding to the provided symbol.", false);
            pricesEmbed.AddField("!s {TOKEN_ID}", "Displays data of a token corresponding to the provided token ID.", false);
            await ctx.Channel.SendMessageAsync(embed: pricesEmbed).ConfigureAwait(false);

            DiscordEmoji getEmoji(string name)
            {
                switch (name)
                {
                    case "gloves":
                        return DiscordEmoji.FromName(ctx.Client, $":{"penancegloves"}:");
                    case "queen":
                        return DiscordEmoji.FromName(ctx.Client, $":{"penancequeen"}:");
                    case "boots":
                        return DiscordEmoji.FromName(ctx.Client, $":{"runnerboots"}:");
                    case "hat":
                        return DiscordEmoji.FromName(ctx.Client, $":{"fighterhat"}:");
                    case "skirt":
                        return DiscordEmoji.FromName(ctx.Client, $":{"penanceskirt"}:");
                    default:
                        return DiscordEmoji.FromName(ctx.Client, $":{name}:");
                }
            }
        }
    }
}

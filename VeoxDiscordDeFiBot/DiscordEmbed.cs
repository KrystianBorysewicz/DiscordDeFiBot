using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace VeoxDiscordDeFiBot
{
    class DiscordEmbed
    {
        public static DiscordEmbedBuilder TokenStatsAllEmbed(Token token)
        {
            if (token == null)
                return null;

            var pricesEmbed = new DiscordEmbedBuilder
            {
                Title = $"📜 ALL {token.symbol} STATS 📜",
                Color = DiscordColor.PhthaloGreen,
            };
            foreach (PropertyInfo prop in token.GetType().GetProperties())
            {
                pricesEmbed.AddField(prop.Name.AddSpacesBeforeCapitals().ToUpperInvariant(), prop.GetValue(token).ToString(), true);
            }

            return pricesEmbed;
        }
        public static DiscordEmbedBuilder TokenStatsEmbed(Token token)
        {
            if (token == null)
                return null;
            var pricesEmbed = new DiscordEmbedBuilder
            {
                Title = $"📊 {token.symbol} STATS 📊",
                Color = DiscordColor.PhthaloGreen,
            };

            pricesEmbed.AddField("✅ Symbol", token.symbol, true);
            pricesEmbed.AddField("💵 USD Price", "$" + token.ethPrice.ToString("#,##0.00000"), true);
            pricesEmbed.AddField("🔤 Name", token.name, true);
            pricesEmbed.AddField("💳 ID", token.id, true);
            pricesEmbed.AddField("🔗 Link", $"[Uniswap](https://uniswap.info/token/{token.id})", true);
            pricesEmbed.AddField("🌊 Liquidity", "$" + Convert.ToInt32(Convert.ToDouble(token.totalLiquidity) * token.ethPrice).ToString("#,##0"), true);

            pricesEmbed.Footer = new DiscordEmbedBuilder.EmbedFooter
            {
                IconUrl = "https://i.imgur.com/n22Zxvj.png",
                Text = "Source: Uniswap.org"
            };

            return pricesEmbed;
        }

        public static List<DiscordEmbedBuilder> TokenStatsEmbeds(List<Token> tokens)
        {
            var embeds = new List<DiscordEmbedBuilder>();
            foreach(var token in tokens)
            {
                if (token == null)
                    return null;
                var pricesEmbed = new DiscordEmbedBuilder
                {
                    Title = $"📊 {token.symbol} STATS 📊",
                    Color = DiscordColor.PhthaloGreen,
                };

                pricesEmbed.AddField("✅ Symbol", token.symbol, true);
                pricesEmbed.AddField("💵 USD Price", "$" + token.ethPrice.ToString("#,##0.00000"), true);
                pricesEmbed.AddField("🔤 Name", token.name, true);
                pricesEmbed.AddField("💳 ID", token.id, true);
                pricesEmbed.AddField("🔗 Link", $"[Uniswap](https://uniswap.info/token/{token.id})", true);
                pricesEmbed.AddField("🌊 Liquidity", "$" + Convert.ToInt32(Convert.ToDouble(token.totalLiquidity) * token.ethPrice).ToString("#,##0"), true);

                pricesEmbed.Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    IconUrl = "https://i.imgur.com/n22Zxvj.png",
                    Text = "Source: Uniswap.org"
                };
                embeds.Add(pricesEmbed);
            }
            return embeds;
        }

    }
}

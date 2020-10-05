using System;

namespace VeoxDiscordDeFiBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Prices.ETH = 350;
            var priceUpdater = new PriceUpdater();
            var bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}

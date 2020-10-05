using System;
using System.Collections.Generic;
using System.Text;

namespace VeoxDiscordDeFiBot
{
    class Token
    {
        public string decimals { get; set; }
        public string derivedETH { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string totalLiquidity { get; set; }
        public string totalSupply { get; set; }
        public string tradeVolume { get; set; }
        public string tradeVolumeUSD { get; set; }
        public string txCount { get; set; }
        public string untrackedVolumeUSD { get; set; }
        public double ethPrice { get { return Convert.ToDouble(derivedETH) * Prices.ETH; } }
    }
}

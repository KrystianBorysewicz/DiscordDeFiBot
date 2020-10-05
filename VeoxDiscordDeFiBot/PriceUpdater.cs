using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Timers;

namespace VeoxDiscordDeFiBot
{
    class PriceUpdater
    {
        private Timer timer1;

        public PriceUpdater()
        {
            InitTimer();
        }

        public void InitTimer()
        {
            timer1 = new System.Timers.Timer();
            timer1.Interval = 45000;
            timer1.Elapsed += OnTimedEvent;
            timer1.AutoReset = true;
            timer1.Enabled = true;
        }

        private async void OnTimedEvent(object sender, EventArgs e)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.coingecko.com/api/v3/simple/price?ids=ethereum&vs_currencies=usd")
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get
            };

            JObject responseObject;

            using (var response = await httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                responseObject = JObject.Parse(JObject.Parse(responseString)["ethereum"].ToString());
            }
            Prices.ETH = Convert.ToDouble(responseObject["usd"]);
        }
    }
}

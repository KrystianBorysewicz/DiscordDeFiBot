using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VeoxDiscordDeFiBot
{
    class TheGraph
    {
        private static HttpClient httpClient = new HttpClient() {
            BaseAddress = new Uri("https://api.thegraph.com/subgraphs/name/ianlapham/uniswapv2")
        };

        public static async Task<Token> GetTokenByID(string tokenID)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://api.thegraph.com/subgraphs/name/ianlapham/uniswapv2")
            };

            var queryObject = new
            {
                query = $@"{{
                    token(id: ""{tokenID}"") {{
                        id
                        symbol
                        name
                        decimals
                        totalSupply
                        tradeVolume
                        tradeVolumeUSD
                        untrackedVolumeUSD
                        txCount
                        totalLiquidity
                        derivedETH
                    }}
                }}",
                variables = new { }
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(queryObject))
            };

            Token responseToken;

            using (var response = await httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                responseToken = JsonConvert.DeserializeObject<TokenMessage>(responseString).data.token;
            }
            return responseToken;
        }

        public static async Task<List<Token>> GetTokensBySymbol(string symbol)
        {

            var queryObject = new
            {
                query = $@"{{
                    tokens(first: 5, where: {{symbol_in: [""{symbol}""], totalLiquidity_gt: 1, derivedETH_gt: 0}}, orderBy: totalLiquidity, orderDirection: desc) {{
                        id
                        symbol
                        name
                        decimals
                        totalSupply
                        tradeVolume
                        tradeVolumeUSD
                        untrackedVolumeUSD
                        txCount
                        totalLiquidity
                        derivedETH
                    }}
                }}",
                variables = new { }
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(queryObject))
            };

            List<Token> responseTokens;

            using (var response = await httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                responseTokens = JsonConvert.DeserializeObject<TokenMessage>(responseString).data.tokens;
            }
            return responseTokens;
        }
    }
}

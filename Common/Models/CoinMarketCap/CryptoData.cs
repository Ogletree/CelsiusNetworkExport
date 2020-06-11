using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Models.CoinMarketCap
{
    public class CryptoData
    {
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, Datum> Data { get; set; }
    }
}
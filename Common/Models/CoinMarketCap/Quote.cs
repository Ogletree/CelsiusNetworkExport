using Newtonsoft.Json;

namespace Common.Models.CoinMarketCap
{
    public class Quote
    {
        [JsonProperty("CAD")]
        public Cad Cad { get; set; }
    }
}
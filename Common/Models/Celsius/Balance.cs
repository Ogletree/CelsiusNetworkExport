using Newtonsoft.Json;

namespace Common.Models.Celsius
{
    public class Balance
    {
#pragma warning disable IDE1006 // Naming Styles
        public string eth { get; set; }
        public string btc { get; set; }
        public string dash { get; set; }
        public string bch { get; set; }
        public string bsv { get; set; }
        public string ltc { get; set; }
        public string zec { get; set; }
        public string btg { get; set; }
        public string xrp { get; set; }
        public string xlm { get; set; }
        public string omg { get; set; }
        public string tusd { get; set; }
        public string gusd { get; set; }
        public string pax { get; set; }
        public string usdc { get; set; }
        public string dai { get; set; }
        public string mcdai { get; set; }
        public string cel { get; set; }
        public string zrx { get; set; }
        public string orbs { get; set; }
        [JsonProperty("usdt erc20")]
        public string usdt { get; set; }
        public string tgbp { get; set; }
        public string taud { get; set; }
        public string thkd { get; set; }
        public string tcad { get; set; }
        public string eos { get; set; }
        public string sga { get; set; }
        public string xaut { get; set; }
        public string etc { get; set; }
        public string bat { get; set; }
        public string busd { get; set; }
        public string knc { get; set; }
        public string link { get; set; }
        public string lpt { get; set; }
        public string matic { get; set; }
        public string snx { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Models.Celsius
{
    public class CelsiusBalance
    {
        [JsonProperty("balance")]
        public Balance Balance { get; set; }

        public IEnumerable<Balances> GetBalances()
        {
            yield return new Balances {Symbol = "ETH", Amount = Convert.ToDecimal(Balance.eth)};
            yield return new Balances { Symbol = "BTC", Amount = Convert.ToDecimal(Balance.btc) };
            yield return new Balances { Symbol = "DASH", Amount = Convert.ToDecimal(Balance.dash) };
            yield return new Balances { Symbol = "BCH", Amount = Convert.ToDecimal(Balance.bch) };
            yield return new Balances { Symbol = "BSV", Amount = Convert.ToDecimal(Balance.bsv) };
            yield return new Balances { Symbol = "LTC", Amount = Convert.ToDecimal(Balance.ltc) };
            yield return new Balances { Symbol = "ZEC", Amount = Convert.ToDecimal(Balance.zec) };
            yield return new Balances { Symbol = "BTG", Amount = Convert.ToDecimal(Balance.btg) };
            yield return new Balances { Symbol = "XRP", Amount = Convert.ToDecimal(Balance.xrp) };
            yield return new Balances { Symbol = "XLM", Amount = Convert.ToDecimal(Balance.xlm) };
            yield return new Balances { Symbol = "OMG", Amount = Convert.ToDecimal(Balance.omg) };
            yield return new Balances { Symbol = "TUSD", Amount = Convert.ToDecimal(Balance.tusd) };
            yield return new Balances { Symbol = "GUSD", Amount = Convert.ToDecimal(Balance.gusd) };
            yield return new Balances { Symbol = "PAX", Amount = Convert.ToDecimal(Balance.pax) };
            yield return new Balances { Symbol = "USDC", Amount = Convert.ToDecimal(Balance.usdc) };
            yield return new Balances { Symbol = "DAI", Amount = Convert.ToDecimal(Balance.dai) };
            yield return new Balances { Symbol = "MCDAI", Amount = Convert.ToDecimal(Balance.mcdai) };
            yield return new Balances { Symbol = "CEL", Amount = Convert.ToDecimal(Balance.cel) };
            yield return new Balances { Symbol = "ZRX", Amount = Convert.ToDecimal(Balance.zrx) };
            yield return new Balances { Symbol = "ORBS", Amount = Convert.ToDecimal(Balance.orbs) };
            yield return new Balances { Symbol = "USDT", Amount = Convert.ToDecimal(Balance.usdt) };
            yield return new Balances { Symbol = "TGBP", Amount = Convert.ToDecimal(Balance.tgbp) };
            yield return new Balances { Symbol = "TAUD", Amount = Convert.ToDecimal(Balance.taud) };
            yield return new Balances { Symbol = "THKD", Amount = Convert.ToDecimal(Balance.thkd) };
            yield return new Balances { Symbol = "TCAD", Amount = Convert.ToDecimal(Balance.tcad) };
            yield return new Balances { Symbol = "EOS", Amount = Convert.ToDecimal(Balance.eos) };
            yield return new Balances { Symbol = "SGA", Amount = Convert.ToDecimal(Balance.sga) };
            yield return new Balances { Symbol = "XAUT", Amount = Convert.ToDecimal(Balance.xaut) };
            yield return new Balances { Symbol = "ETC", Amount = Convert.ToDecimal(Balance.etc) };
            yield return new Balances { Symbol = "BAT", Amount = Convert.ToDecimal(Balance.bat) };
            yield return new Balances { Symbol = "BUSD", Amount = Convert.ToDecimal(Balance.busd) };
            yield return new Balances { Symbol = "KNC", Amount = Convert.ToDecimal(Balance.knc) };
            yield return new Balances { Symbol = "LINK", Amount = Convert.ToDecimal(Balance.link) };
            yield return new Balances { Symbol = "LPT", Amount = Convert.ToDecimal(Balance.lpt) };
            yield return new Balances { Symbol = "MATIC", Amount = Convert.ToDecimal(Balance.matic) };
            yield return new Balances { Symbol = "SNX", Amount = Convert.ToDecimal(Balance.snx) };
        }
    }

    public class Balances
    {
        public string Symbol { get; set; }
        public decimal Amount { get; set; }
    }
}
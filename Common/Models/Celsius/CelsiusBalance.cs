using System;
using System.Collections.Generic;

namespace Common.Models.Celsius
{
    public class CelsiusBalance
    {
        public Balance balance { get; set; }

        public IEnumerable<Balances> GetBalances()
        {
            yield return new Balances {symbol = "ETH", amount = Convert.ToDecimal(balance.eth)};
            yield return new Balances { symbol = "BTC", amount = Convert.ToDecimal(balance.btc) };
            yield return new Balances { symbol = "DASH", amount = Convert.ToDecimal(balance.dash) };
            yield return new Balances { symbol = "BCH", amount = Convert.ToDecimal(balance.bch) };
            yield return new Balances { symbol = "LTC", amount = Convert.ToDecimal(balance.ltc) };
            yield return new Balances { symbol = "ZEC", amount = Convert.ToDecimal(balance.zec) };
            yield return new Balances { symbol = "BTG", amount = Convert.ToDecimal(balance.btg) };
            yield return new Balances { symbol = "XRP", amount = Convert.ToDecimal(balance.xrp) };
            yield return new Balances { symbol = "XLM", amount = Convert.ToDecimal(balance.xlm) };
            yield return new Balances { symbol = "OMG", amount = Convert.ToDecimal(balance.omg) };
            yield return new Balances { symbol = "TUSD", amount = Convert.ToDecimal(balance.tusd) };
            yield return new Balances { symbol = "GUSD", amount = Convert.ToDecimal(balance.gusd) };
            yield return new Balances { symbol = "PAX", amount = Convert.ToDecimal(balance.pax) };
            yield return new Balances { symbol = "USDC", amount = Convert.ToDecimal(balance.usdc) };
            yield return new Balances { symbol = "DAI", amount = Convert.ToDecimal(balance.dai) };
            yield return new Balances { symbol = "CEL", amount = Convert.ToDecimal(balance.cel) };
            yield return new Balances { symbol = "ZRX", amount = Convert.ToDecimal(balance.zrx) };
            yield return new Balances { symbol = "ORBS", amount = Convert.ToDecimal(balance.orbs) };
            yield return new Balances { symbol = "USDT", amount = Convert.ToDecimal(balance.usdt) };
            yield return new Balances { symbol = "EOS", amount = Convert.ToDecimal(balance.eos) };
        }
    }

    public class Balances
    {
        public string symbol { get; set; }
        public decimal amount { get; set; }
    }
}
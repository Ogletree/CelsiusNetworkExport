using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CoinpaprikaAPI.Entity;
using CoinpaprikaAPI.Models;

namespace Common.Controllers
{
    public class ExchangeDataCoinPaprika : IExchangeData
    {
        private static readonly string QuoteCurrency = ConfigurationManager.AppSettings["QuoteCurrency"];
        private readonly CoinPaprikaEntity<List<TickerWithQuotesInfo>> _quoteData;
        public string Id => "CoinPaprika";

        public ExchangeDataCoinPaprika()
        {
            var client = new CoinpaprikaAPI.Client();
            _quoteData = client.GetTickersAsync(new[] { QuoteCurrency }).Result;
        }

        public string GetName(string symbol)
        {
            var quotesInfo = _quoteData.Value.FirstOrDefault(x => x.Symbol == symbol.ToUpper());
            return quotesInfo?.Name ?? symbol;
        }

        public decimal GetPrice(string symbol)
        {
            var quotesInfo = _quoteData.Value.FirstOrDefault(x => x.Symbol == symbol.ToUpper());
            return quotesInfo?.Quotes[QuoteCurrency].Price ?? 0m;
        }

        public decimal GetPercentChange24Hour(string symbol)
        {
            var quotesInfo = _quoteData.Value.FirstOrDefault(x => x.Symbol == symbol.ToUpper());
            return quotesInfo?.Quotes[QuoteCurrency].PercentChange24H ?? 0m;
        }
    }
}
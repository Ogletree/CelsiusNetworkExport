using System;
using System.Configuration;
using System.Reflection;
using log4net;
using Newtonsoft.Json;
using RestSharp;

namespace Common.Controllers
{
    public class ExchangeDataCoinMarketCap : IExchangeData
    {
        private static dynamic _quoteData;
        private readonly RestClient _client = new RestClient("https://pro-api.coinmarketcap.com");
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly string QuoteCurrency = ConfigurationManager.AppSettings["QuoteCurrency"];
        private static readonly string ApiKey = ConfigurationManager.AppSettings["CoinMarketCapApi"];

        public string Id => "CoinMarketCap";

        public ExchangeDataCoinMarketCap()
        {
            GetQuotes();
        }

        public string GetName(string symbol)
        {
            return _quoteData.data[symbol.ToUpper()].name;
        }

        public decimal GetPrice(string symbol)
        {
            return _quoteData.data[symbol.ToUpper()].quote[QuoteCurrency].price;
        }

        public decimal GetPercentChange24Hour(string symbol)
        {
            return _quoteData.data[symbol.ToUpper()].quote[QuoteCurrency].percent_change_24h;
        }
        public void GetQuotes()
        {
            var request = new RestRequest("/v1/cryptocurrency/quotes/latest", Method.GET);
            request.AddHeader("X-CMC_PRO_API_KEY", ApiKey);
            request.AddHeader("Accepts", "application/json");
            request.AddParameter("symbol", "ETH,BTC,DASH,BCH,LTC,ZEC,BTG,XRP,XLM,OMG,TUSD,GUSD,PAX,USDC,DAI,CEL,ZRX,ORBS,USDT,EOS"); //TODO: This shouldn't be hardcoded
            request.AddParameter("convert", QuoteCurrency);
            var restResponse = _client.Execute(request);
            try
            {
                _quoteData = JsonConvert.DeserializeObject<dynamic>(restResponse.Content);
            }
            catch (Exception)
            {
                Log.Debug("CoinMarketCap Response:");
                Log.Debug(restResponse.Content);
                throw;
            }
        }
    }
}
using System;
using System.Configuration;
using System.Reflection;
using Common.Models.CoinMarketCap;
using log4net;
using Newtonsoft.Json;
using RestSharp;

namespace Common.Controllers
{
    public class CoinMarketCap
    {
        private readonly RestClient _client = new RestClient("https://pro-api.coinmarketcap.com");
        private static readonly string Apikey = ConfigurationManager.AppSettings["CoinMarketCapApi"];
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public CryptoData GetAccounts()
        {
            var request = new RestRequest("/v1/cryptocurrency/quotes/latest", Method.GET);
            request.AddHeader("X-CMC_PRO_API_KEY", Apikey);
            request.AddHeader("Accepts", "application/json");
            request.AddParameter("symbol", "ETH,BTC,DASH,BCH,LTC,ZEC,BTG,XRP,XLM,OMG,TUSD,GUSD,PAX,USDC,DAI,CEL,ZRX,ORBS,USDT,EOS"); //TODO: This shouldn't be hardcoded
            request.AddParameter("convert", "CAD");
            var restResponse = _client.Execute(request);
            try
            {
                var crypto = JsonConvert.DeserializeObject<CryptoData>(restResponse.Content);
                return crypto;
            }
            catch (Exception)
            {
                Log.Debug("CoinMarketCap Response:");
                Log.Debug(restResponse.Content);
                return null;
            }
        }
    }
}
using System;
using System.Configuration;
using System.Reflection;
using Common.Models.Celsius;
using log4net;
using Newtonsoft.Json;
using RestSharp;

namespace Common.Controllers
{
    public class Celsius
    {
        private readonly string _apiKey;
        private readonly RestClient _client = new RestClient("https://wallet-api.celsius.network");
        private static readonly string PartnerToken = ConfigurationManager.AppSettings["PartnerToken"];
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Celsius(string apiKey)
        {
            _apiKey = apiKey;
        }

        public CelsiusBalance GetBalance()
        {
            var request = new RestRequest("/wallet/balance", Method.GET);
            request.AddHeader("X-Cel-Partner-Token", PartnerToken);
            request.AddHeader("X-Cel-Api-Key", _apiKey);
            var restResponse = _client.Execute(request);
            try
            {
                var crypto = JsonConvert.DeserializeObject<CelsiusBalance>(restResponse.Content);
                return crypto;
            }
            catch (Exception)
            {
                Log.Debug("Celsius Response:");
                Log.Debug(restResponse.Content);
                return null;
            }
        }
        public Transactions GetTransactions()
        {
            var request = new RestRequest("/wallet/transactions?page=1&per_page=100000", Method.GET);
            request.AddHeader("X-Cel-Partner-Token", PartnerToken);
            request.AddHeader("X-Cel-Api-Key", _apiKey);
            var restResponse = _client.Execute(request);
            try
            {
                var crypto = JsonConvert.DeserializeObject<Transactions>(restResponse.Content);
                return crypto;
            }
            catch (Exception)
            {
                Log.Debug("Celsius Response:");
                Log.Debug(restResponse.Content);
                return null;
            }
        }
    }
}
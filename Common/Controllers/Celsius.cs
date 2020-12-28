using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using Common.Models.Celsius;
using log4net;
using Newtonsoft.Json;
using RestSharp;

namespace Common.Controllers
{
    public class Celsius
    {
        private readonly string _apiKey;
        private readonly string _partnerToken;
        private readonly RestClient _client = new RestClient("https://wallet-api.celsius.network");
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Celsius(string apiKey, string partnerToken)
        {
            _apiKey = apiKey;
            _partnerToken = partnerToken;
        }

        public CelsiusBalance GetBalance()
        {
            var request = new RestRequest("/wallet/balance", Method.GET);
            request.AddHeader("X-Cel-Partner-Token", _partnerToken);
            request.AddHeader("X-Cel-Api-Key", _apiKey);
            var restResponse = _client.Execute(request);
            try
            {
                var crypto = JsonConvert.DeserializeObject<CelsiusBalance>(restResponse.Content);
                File.WriteAllText("Balance.json", restResponse.Content);
                return crypto;
            }
            catch (Exception)
            {
                Log.Debug("Celsius Response:");
                Log.Debug(restResponse.Content);
                throw new InvalidDataContractException("Unexpected content in CelsiusBalance response content.");
            }
        }
        public Transactions GetTransactions()
        {
            var request = new RestRequest("/wallet/transactions?page=1&per_page=100000", Method.GET);
            request.AddHeader("X-Cel-Partner-Token", _partnerToken);
            request.AddHeader("X-Cel-Api-Key", _apiKey);
            var restResponse = _client.Execute(request);
            try
            {
                var crypto = JsonConvert.DeserializeObject<Transactions>(restResponse.Content);
                File.WriteAllText("Transactions.json", restResponse.Content);
                return crypto;
            }
            catch (Exception)
            {
                Log.Debug("Celsius Response:");
                Log.Debug(restResponse.Content);
                throw new InvalidDataContractException("Unexpected content in CelsiusTransactions response content.");
            }
        }
    }
}
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Common.Controllers;
using Common.Models.Celsius;
using Common.Models.CoinMarketCap;

namespace Extract
{
    public class CelsiusTransactionExport
    {
        private static GoogleSheet _sheet;
        private static readonly string spreadSheet = ConfigurationManager.AppSettings["SpreadSheetId"];
        private static readonly string apiKey = ConfigurationManager.AppSettings["CelsiusApiKey"];
        private static CryptoData cryptoData;
        public static void Run()
        {
            var coinMarketCap = new CoinMarketCap();
            cryptoData = coinMarketCap.GetAccounts();

            var celsius = new Celsius(apiKey);
            var balance = celsius.GetBalance();
            var transactions = celsius.GetTransactions();
            DoIt(spreadSheet, balance);
            DoEeeet(spreadSheet, transactions);
        }

        private static void DoEeeet(string sheetId, Transactions transactions)
        {
            _sheet = new GoogleSheet(sheetId);
            IList<IList<object>> stuff = new List<IList<object>>();
            foreach (var record in transactions.record.OrderBy(x => x.time))
            {
                IList<object> stuffs = new List<object>();
                if (record.amount == "0.000000000000000000")
                    continue;
                stuffs.Add(record.time);
                var type = "";
                switch (record.nature)
                {
                    case "deposit":
                        type = "Buy";
                        break;
                    case "withdrawal":
                        type = "Sell";
                        break;
                    case "interest":
                        type = "Div";
                        break;
                    case "bonus_token":
                        type = "Buy";
                        break;
                    case "outbound_transfer":
                        type = "Sell";
                        break;
                    case "referrer_award":
                        type = "Buy";
                        break;
                    case "inbound_transfer":
                        type = "Buy";
                        break;
                }
                stuffs.Add(type);
                var datum = cryptoData.Data[record.coin];
                stuffs.Add(datum.Name);
                stuffs.Add(record.amount.Replace("-", ""));
                if (record.nature == "bonus_token")
                    stuffs.Add("Bonus Token");
                if (record.nature == "outbound_transfer")
                    stuffs.Add("Outbound Transfer");
                if (record.nature == "referrer_award")
                    stuffs.Add("Referrer Award");
                if (record.nature == "inbound_transfer")
                    stuffs.Add("Inbound Transfer");
                stuff.Add(stuffs);
            }
            _sheet.WriteStuff(stuff, "Transactions!A2");

        }

        private static void DoIt(string sheetId, CelsiusBalance balance)
        {

            _sheet = new GoogleSheet(sheetId);
            IList<IList<object>> stuff = new List<IList<object>>();
            foreach (var balances in balance.GetBalances())
            {
                IList<object> stuffs = new List<object>();
                stuffs.Add(balances.symbol);

                var datum = cryptoData.Data[balances.symbol.ToUpper()];
                stuffs.Add(datum.Quote.Cad.Price);
                stuffs.Add(datum.Quote.Cad.PercentChange24H);
                stuffs.Add(datum.Name);
                stuff.Add(stuffs);
            }
            _sheet.WriteStuff(stuff, "Summary!A3");
        }
    }
}
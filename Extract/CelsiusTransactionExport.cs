using System;
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
        private static readonly string CelApiKey = ConfigurationManager.AppSettings["CelsiusApiKey"];
        private static readonly string CelPartnerToken = ConfigurationManager.AppSettings["PartnerToken"];
        private static readonly string CmcApikey = ConfigurationManager.AppSettings["CoinMarketCapApi"];
        private static readonly string SpreadSheetId = ConfigurationManager.AppSettings["SpreadSheetId"];

        private static GoogleSheet _sheet;
        private static CryptoData _cryptoData;
        public static void Run()
        {
            var coinMarketCap = new CoinMarketCap(CmcApikey);
            _cryptoData = coinMarketCap.GetAccounts();

            var celsius = new Celsius(CelApiKey, CelPartnerToken);
            var balance = celsius.GetBalance();
            var transactions = celsius.GetTransactions();

            _sheet = new GoogleSheet(SpreadSheetId);

            ProcessBalance(balance);
            ProcessTransactions(transactions);
        }
        private static void ProcessBalance(CelsiusBalance balance)
        {
            IList<IList<object>> rows = new List<IList<object>>();
            foreach (var balances in balance.GetBalances())
            {
                IList<object> columns = new List<object>();
                columns.Add(balances.symbol);

                var datum = _cryptoData.Data[balances.symbol.ToUpper()];
                columns.Add(datum.Quote.Cad.Price);
                columns.Add(datum.Quote.Cad.PercentChange24H);
                columns.Add(datum.Name);
                rows.Add(columns);
            }
            _sheet.WriteStuff(rows, "Summary!A3");
        }
        private static void ProcessTransactions(Transactions transactions)
        {
            IList<IList<object>> rows = new List<IList<object>>();
            foreach (var record in transactions.record.OrderBy(x => x.time))
            {
                IList<object> columns = new List<object>();
                if (record.amount == "0.000000000000000000")
                    continue;
                columns.Add(record.time);
                string type;
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
                    default:
                        throw new NotImplementedException($"New record nature: {record.nature}");
                }
                columns.Add(type);
                var datum = _cryptoData.Data[record.coin];
                columns.Add(datum.Name);
                columns.Add(record.amount.Replace("-", ""));
                switch (record.nature)
                {
                    case "bonus_token":
                        columns.Add("Bonus Token");
                        break;
                    case "outbound_transfer":
                        columns.Add("Outbound Transfer");
                        break;
                    case "referrer_award":
                        columns.Add("Referrer Award");
                        break;
                    case "inbound_transfer":
                        columns.Add("Inbound Transfer");
                        break;
                    default:
                        throw new NotImplementedException($"New record nature: {record.nature}");
                }
                rows.Add(columns);
            }
            _sheet.WriteStuff(rows, "Transactions!A2");
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Common.Controllers;
using Common.Models.Celsius;

namespace Extract
{
    public class CelsiusTransactionExport
    {
        private static readonly string CelApiKey = ConfigurationManager.AppSettings["CelsiusApiKey"];
        private static readonly string CelPartnerToken = ConfigurationManager.AppSettings["PartnerToken"];
        private static readonly string SpreadSheetId = ConfigurationManager.AppSettings["SpreadSheetId"];
        private static IExchangeData _exchangeData;
        private static GoogleSheet _sheet;
        public static void Run()
        {
            _exchangeData = new ExchangeDataCoinPaprika();
            _sheet = new GoogleSheet(SpreadSheetId);

            var celsius = new Celsius(CelApiKey, CelPartnerToken);
            var balance = celsius.GetBalance();
            var transactions = celsius.GetTransactions();

            ProcessBalance(balance);
            ProcessTransactions(transactions);
        }
        private static void ProcessBalance(CelsiusBalance balance)
        {
            IList<IList<object>> rows = new List<IList<object>>();
            foreach (var balances in balance.GetBalances().OrderByDescending(x=>x.Amount).ThenBy(x=>x.Symbol))
            {
                IList<object> columns = new List<object>();
                columns.Add(balances.Symbol);
                columns.Add(_exchangeData.GetPrice(balances.Symbol));
                columns.Add(_exchangeData.GetPercentChange24Hour(balances.Symbol));
                columns.Add(_exchangeData.GetName(balances.Symbol));
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
                if(record.state != "confirmed")
                    continue;
                if (record.nature == "")
                    continue;
                if (record.nature == "collateral") continue;
                if (record.nature == "loan_principal_payment") continue;
                if (record.nature == "loan_interest_payment") continue;
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
                columns.Add(_exchangeData.GetName(record.coin));
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
                }
                rows.Add(columns);
            }
            _sheet.WriteStuff(rows, "Transactions!A2");
        }
    }
}
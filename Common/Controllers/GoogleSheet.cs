using System.Collections.Generic;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;

namespace Common.Controllers
{
    public class GoogleSheet
    {
        private readonly string _spreadsheetId;
        private readonly SheetsService _service;

        public GoogleSheet(string spreadsheetId)
        {
            _spreadsheetId = spreadsheetId;
            UserCredential credential;
            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                const string credPath = "token.json";
                string[] scopes = { SheetsService.Scope.Spreadsheets };
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets,
                    scopes, "user", CancellationToken.None, new FileDataStore(credPath, true)).Result;
            }
            _service = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "MyMoney",
            });
        }
        public void WriteStuff(IList<IList<object>> stuff, string range)
        {
            var valueRange = new ValueRange { MajorDimension = "ROWS", Values = stuff };
            var update = _service.Spreadsheets.Values.Update(valueRange, _spreadsheetId, range);
            update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            update.Execute();
        }

        public IList<IList<object>> ReadStuff(string range)
        {
            var request = _service.Spreadsheets.Values.Get(_spreadsheetId, range);
            var response = request.Execute();
            var values = response.Values;
            if (values == null || values.Count <= 0) return values;
            return values;
        }
    }
}
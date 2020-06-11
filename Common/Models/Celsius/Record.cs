using System;

namespace Common.Models.Celsius
{
    public class Record
    {
        public string amount { get; set; }
        public object amount_usd { get; set; }
        public string coin { get; set; }
        public string state { get; set; }
        public string nature { get; set; }
        public DateTime time { get; set; }
        public string tx_id { get; set; }
    }
}
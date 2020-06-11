using System.Collections.Generic;

namespace Common.Models.Celsius
{
    public class Transactions
    {
        public Pagination pagination { get; set; }
        public List<Record> record { get; set; }
    }
}
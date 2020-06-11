namespace Common.Models.Celsius
{
    public class Pagination
    {
        public int total { get; set; }
        public int pages { get; set; }
        public int current { get; set; }
        public int per_page { get; set; }
        public string showing { get; set; }
    }
}
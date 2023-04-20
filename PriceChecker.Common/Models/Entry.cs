namespace PriceChecker.Common.Models
{
    public class Entry
    {
        public int ID { get; set; }

        public decimal Price { get; set; }

        public DateOnly Date { get; set; }
    }
}
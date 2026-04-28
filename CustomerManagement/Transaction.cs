namespace CustomerManagement
{
    public class Transaction
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime Time { get; set; }


    }
}

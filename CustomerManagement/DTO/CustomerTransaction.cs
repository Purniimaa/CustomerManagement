namespace CustomerManagement.DTO
{
    public class CustomerTransaction
    {
        public int CustomerId { get; set; }
        public int TransactionId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long Phone { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

    }
}

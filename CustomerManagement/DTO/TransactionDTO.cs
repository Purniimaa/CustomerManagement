namespace CustomerManagement.DTO
{
    public class TransactionDTO
    {
       
        public int TransactionId { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }


    }
}

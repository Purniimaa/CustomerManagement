namespace CustomerManagement.DTO
{
    public class TransactionRequest
    {
        public int TransactionId { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public decimal TotalDeposit { get; set; }
        public decimal TotalWithdrawal { get; set; }
    }
}

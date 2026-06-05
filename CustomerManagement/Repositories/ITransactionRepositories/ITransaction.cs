using CustomerManagement.DTO;

namespace CustomerManagement.Repositories.ITransactionRepositories
{
    public interface ITransaction
    {
        //Task<int> Login(LoginDTO login);
        Task<int> Deposit(decimal amount, int id);

        Task<int> Withdraw(decimal amount, int id);

        Task<List<TransactionDTO>> GetAllTransactions();
        Task<TransactionDTO> GetTransactionById(int id);
       Task<decimal> TotalDeposit(int CustomerId);
        Task<decimal> TotalWithdraw(int CustomerId);
        Task<List<CustomerTransaction>> CusDeposit(int id);

        Task<List<CustomerTransaction>> CusWithdraw(int id);


    }
}

// Name: Peter Do
// Student Number: 9086580

using BankingSystem.Models;

namespace BankingSystem.Repositories;

public interface IBankRepository
{
    IEnumerable<Account> GetAllAccounts();
    Account? GetAccountById(int accountId);
    Account? GetAccountByNumber(string accountNumber);
    void UpdateAccount(Account account);
    
    IEnumerable<Transaction> GetTransactionsForAccount(int accountId);
    void AddTransaction(Transaction transaction);
    
    bool SaveChanges();
}

// Name: Peter Do
// Student Number: 9086580

using BankingSystem.Models;
using BankingSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using BankingSystem.Data;

public class BankRepository : IBankRepository
{
    private readonly BankingDbContext _context;

    public BankRepository(BankingDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Account> GetAllAccounts()
    {
        return _context.Accounts.Include(a => a.Customer).ToList();
    }

    public Account? GetAccountById(int accountId)
    {
        return _context.Accounts
            .Include(a => a.Customer)
            .FirstOrDefault(a => a.AccountId == accountId);
    }

    public Account? GetAccountByNumber(string accountNumber)
    {
        return _context.Accounts
            .FirstOrDefault(a => a.AccountNumber == accountNumber);
    }

    public void UpdateAccount(Account account)
    {
        _context.Accounts.Update(account);
    }

    public IEnumerable<Transaction> GetTransactionsForAccount(int accountId)
    {
        return _context.Transactions
            .Where(t => t.AccountId == accountId)
            .OrderByDescending(t => t.TransactionDate)
            .ToList();
    }

    public void AddTransaction(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() >= 0;
    }

}
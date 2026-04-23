// Name: Peter Do
// Student Number: 9086580

using BankingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Data;

public class BankingDbContext : DbContext
{
    public BankingDbContext(DbContextOptions<BankingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data for testing
        modelBuilder.Entity<Customer>().HasData(
            new Customer { CustomerId = 1, FirstName = "Peter", LastName = "Do", Email = " peter@example.com" },
            new Customer { CustomerId = 2, FirstName = "John", LastName = "Smith", Email = "john@example.com" }
        );

        modelBuilder.Entity<Account>().HasData(
            new Account { AccountId = 1, CustomerId = 1, AccountNumber = "ACC-12345", AccountType = AccountType.Checking, Balance = 1500m },
            new Account { AccountId = 2, CustomerId = 1, AccountNumber = "ACC-12346", AccountType = AccountType.Savings, Balance = 5000m },
            new Account { AccountId = 3, CustomerId = 2, AccountNumber = "ACC-67890", AccountType = AccountType.Checking, Balance = 250m }
        );
    }
}

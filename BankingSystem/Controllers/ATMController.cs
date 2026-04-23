// Name: Peter Do
// Student Number: 9086580

using BankingSystem.DTOs;
using BankingSystem.Models;
using BankingSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers;

[ApiController]
[Route("api/atm")]
public class AtmController : ControllerBase
{
    private readonly IBankRepository _repository;

    public AtmController(IBankRepository repository)
    {
        _repository = repository;
    }

    // GET: api/atm/balance/{accountId}
    [HttpGet("balance/{accountId}")]
    public IActionResult GetBalance(int accountId)
    {
        var account = _repository.GetAccountById(accountId);
        if (account == null) return NotFound(new { error = "Account does not exist" });

        return Ok(new BalanceResponse
        {
            AccountId = account.AccountId,
            AccountNumber = account.AccountNumber,
            Balance = account.Balance
        });
    }

    // POST: api/atm/withdraw
    [HttpPost("withdraw")]
    public IActionResult Withdraw([FromBody] WithdrawRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if(request.Amount <= 0) return BadRequest(new { error = "Amount must be greater than zero" });

        var account = _repository.GetAccountById(request.AccountId);
        if (account == null) return NotFound(new { error = "Account does not exist" });

        if (account.Balance < request.Amount)
        {
            return BadRequest(new { error = "Insufficient funds" });
        }

        account.Balance -= request.Amount;
        var transaction = new Transaction
        {
            AccountId = request.AccountId,
            Amount = request.Amount,
            TransactionType = TransactionType.Withdrawal,
            TransactionDate = DateTime.Now,
            Description = "ATM Withdrawal"
        };

        _repository.UpdateAccount(account);
        _repository.AddTransaction(transaction);
        _repository.SaveChanges();

        return Ok(new
        {
            message = "Withdrawal successful",
            newBalance = account.Balance
        });
    }

    // POST: api/atm/deposit
    [HttpPost("deposit")]
    public IActionResult Deposit([FromBody] DepositRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if(request.Amount <= 0) return BadRequest(new { error = "Amount must be greater than zero" });

        var account = _repository.GetAccountById(request.AccountId);
        if (account == null) return NotFound(new { error = "Account does not exist" });

        account.Balance += request.Amount;
        var transaction = new Transaction
        {
            AccountId = request.AccountId,
            Amount = request.Amount,
            TransactionType = TransactionType.Deposit,
            TransactionDate = DateTime.Now,
            Description = "ATM Deposit"
        };

        _repository.UpdateAccount(account);
        _repository.AddTransaction(transaction);
        _repository.SaveChanges();

        return Ok(new
        {
            message = "Deposit successful",
            newBalance = account.Balance
        });
    }

    // GET: api/atm/transactions/{accountId}
    [HttpGet("transactions/{accountId}")]
    public IActionResult GetTransactions(int accountId)
    {
        var account = _repository.GetAccountById(accountId);
        if (account == null) return NotFound(new { error = "Account does not exist" });

        var transactions = _repository.GetTransactionsForAccount(accountId);
        
        var transactionResponse = transactions.Select(t => new TransactionResponse
        {
            TransactionId = t.TransactionId,
            Amount = t.Amount,
            TransactionType = t.TransactionType.ToString(),
            TransactionDate = t.TransactionDate,
            Description = t.Description ?? string.Empty
        });
        
        return Ok(transactionResponse);
    }
}
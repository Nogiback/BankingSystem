// Name: Peter Do
// Student Number: 9086580

using BankingSystem.Models;
using BankingSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers;

public class TransactionController : Controller
{
    private readonly IBankRepository _repository;

    public TransactionController(IBankRepository repository)
    {
        _repository = repository;
    }

    // GET: /Transaction/Deposit/{accountId}
    [HttpGet]
    public IActionResult Deposit(int accountId)
    {
        var account = _repository.GetAccountById(accountId);
        if (account == null) return NotFound();

        ViewBag.Balance = account.Balance;
        return View(new Transaction { AccountId = accountId, TransactionType = TransactionType.Deposit });
    }

    // POST: /Transaction/Deposit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Deposit(Transaction model)
    {
        var account = _repository.GetAccountById(model.AccountId);
        if (account == null) return NotFound();

        if (model.Amount <= 0)
        {
            ModelState.AddModelError("Amount", "Amount must be greater than zero.");
        }

        if (ModelState.IsValid)
        {
            account.Balance += model.Amount;
            
            model.TransactionType = TransactionType.Deposit;
            model.TransactionDate = DateTime.Now;

            _repository.UpdateAccount(account);
            _repository.AddTransaction(model);
            _repository.SaveChanges();

            TempData["SuccessMessage"] = $"Successfully deposited {model.Amount:C}. New balance is {account.Balance:C}.";
            return RedirectToAction("Details", "Account", new { id = model.AccountId });
        }

        ViewBag.Balance = account.Balance;
        return View(model);
    }

    // GET: /Transaction/Withdraw/{accountId}
    [HttpGet]
    public IActionResult Withdraw(int accountId)
    {
        var account = _repository.GetAccountById(accountId);
        if (account == null) return NotFound();

        ViewBag.Balance = account.Balance;
        return View(new Transaction { AccountId = accountId, TransactionType = TransactionType.Withdrawal });
    }

    // POST: /Transaction/Withdraw
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Withdraw(Transaction model)
    {
        var account = _repository.GetAccountById(model.AccountId);
        if (account == null) return NotFound();

        if (model.Amount <= 0)
        {
            ModelState.AddModelError("Amount", "Amount must be greater than zero.");
        }
        else if (account.Balance < model.Amount)
        {
            ModelState.AddModelError("Amount", "Insufficient funds.");
        }

        if (ModelState.IsValid)
        {
            account.Balance -= model.Amount;
            
            model.TransactionType = TransactionType.Withdrawal;
            model.TransactionDate = DateTime.Now;

            _repository.UpdateAccount(account);
            _repository.AddTransaction(model);
            _repository.SaveChanges();

            TempData["SuccessMessage"] = $"Successfully withdrew {model.Amount:C}. New balance is {account.Balance:C}.";
            return RedirectToAction("Details", "Account", new { id = model.AccountId });
        }

        ViewBag.Balance = account.Balance;
        return View(model);
    }

    // GET: /Transaction/History/{accountId}
    [HttpGet]
    public IActionResult History(int accountId)
    {
        var account = _repository.GetAccountById(accountId);
        if (account == null) return NotFound();

        var transactions = _repository.GetTransactionsForAccount(accountId);
        
        ViewBag.Account = account;
        return View(transactions);
    }
}

// Name: Peter Do
// Student Number: 9086580

using BankingSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers;

public class AccountController : Controller
{
    private readonly IBankRepository _repository;

    public AccountController(IBankRepository repository)
    {
        _repository = repository;
    }

    // GET: /Account/
    public IActionResult Index()
    {
        var accounts = _repository.GetAllAccounts();
        return View(accounts);
    }

    // GET: /Account/Details/{id}
    public IActionResult Details(int id)
    {
        var account = _repository.GetAccountById(id);
        if (account == null)
        {
            return NotFound();
        }
        return View(account);
    }
}

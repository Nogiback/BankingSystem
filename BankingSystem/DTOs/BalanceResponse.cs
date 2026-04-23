// Name: Peter Do
// Student Number: 9086580

namespace BankingSystem.DTOs;

public class BalanceResponse
{
    public int AccountId { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; }
}

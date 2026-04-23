// Name: Peter Do
// Student Number: 9086580

namespace BankingSystem.DTOs;

public class TransactionResponse
{
    public int TransactionId { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; } = string.Empty;
    public DateTime TransactionDate { get; set; }
    public string? Description { get; set; }
}

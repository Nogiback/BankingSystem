// Name: Peter Do
// Student Number: 9086580

using System.ComponentModel.DataAnnotations;

namespace BankingSystem.DTOs;

public class DepositRequest
{
    [Required]
    public int AccountId { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public decimal Amount { get; set; }
}

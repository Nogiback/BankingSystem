using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        public TransactionType TransactionType { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.Now;

        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account Account { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }

    public enum TransactionType
    {
        Deposit,
        Withdrawal
    }
}
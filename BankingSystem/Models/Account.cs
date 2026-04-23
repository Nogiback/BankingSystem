using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.Models
{
    public class Account
    {
        public int AccountId { get; set; }

        [Required]
        [RegularExpression(@"^ACC-\d{5}$", ErrorMessage = "Account number must be in the format 'ACC-XXXXX'")]
        public string AccountNumber { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Balance must be a non-negative value")]
        public decimal Balance { get; set; }

        [Required]
        public AccountType AccountType { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }

        public enum AccountType
        {
            Checking,
            Savings 
        }
    }

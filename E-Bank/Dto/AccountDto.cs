using E_Bank.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Bank.Dto
{
    public class AccountDto
    {
        public int AccountNumber { get; set; }

        public AccountType AccountType { get; set; }
        // public string AccountNumber { get; set; }

        public DateTime OpenningDate { get; set; }
        public double AccountBalance { get; set; }

        public double IntrestRate { get; set; }

        public bool IsActive { get; set; }

       // public Customer Customer { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; } = 0;


        public int TransactionsCount { get; set; } = 0;
    }
}

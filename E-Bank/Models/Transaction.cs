﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Bank.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        public TransactionType TransactionType { get; set; }

        public double TransactionAmount { get; set; }

        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }

        public string State { get; set; }//pending or approved show

        public bool IsActive { get; set; }//soft delete


        public Account Account { get; set; }
        [ForeignKey("Account")]
        public int AccountId { get; set; } = 0;


    }
}

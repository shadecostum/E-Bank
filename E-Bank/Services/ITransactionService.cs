﻿using E_Bank.Dto;
using E_Bank.Models;


namespace E_Bank.Services
{
    public interface ITransactionService
    {
      
        public int Deposite(TransactionDto transaction);

        public int Withdraw(TransactionDto transaction);

        public int TransferAmount(TransferDto transaction);

        public TransactionClass GetById(int id);
        public List<TransactionClass> GetAll();

        public void Delete(TransactionClass transaction);


        //public int Add(TransactionClass transaction);

        // public TransactionClass Update(TransactionClass transaction);
    }
}

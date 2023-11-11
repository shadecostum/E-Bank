﻿using E_Bank.Dto;
using E_Bank.Models;
using Microsoft.EntityFrameworkCore;
using static E_Bank.Repository.IRepository;

namespace E_Bank.Services
{
    
    public class AccountService : IAccountService
    {
            private IRepository<Account> _repository;
            private IRepository<TransactionClass> _transactionClassRepository;

            public AccountService(IRepository<Account> repository,IRepository<TransactionClass> transactionRep)
            {
                _repository = repository;
                _transactionClassRepository = transactionRep;
            }

        public List<Account> GetAll()
        {
            return _repository.GetAll().Where(cus => cus.IsActive)
                     .Include(acnt => acnt.Transactions.Where(acnt => acnt.IsActive == true)).ToList();

        }

        public List<Account> GetAllRequest()
        {
            return _repository.GetAll().Where(acn => acn.IsActive == false)
                              .Include(acn => acn.Customer)
                              .ThenInclude(cus => cus.Documents)
                              .ToList();



        }


        public int ActivateRequest(int accountActivateDto)
        {
          var matched  =  _repository.Get()
                          .Where(acn=>acn.AccountNumber==accountActivateDto && acn.IsActive==false)
                         .FirstOrDefault();
            if(matched==null)
            {
                throw new InvalidOperationException("Account not found");
            }

            matched.IsActive=true;

          if(  _repository.Update(matched) == null )
            {
                throw new InvalidOperationException("Account not found");
            }

            return 1;


        }


        public int Add(Account account)
            {
                return _repository.Add(account);
            }

        public Account GetById(int id)
        {
             var account = _repository.Get()
                      .Where(acn => acn.AccountNumber == id && acn.IsActive)
                      .OrderBy(acn => acn.AccountNumber)
                      .FirstOrDefault();

                if (account != null)
                {
                    _repository.Detached(account);
                }
                return account;

        }

        public Account Update(Account account)
        {
          return _repository.Update(account);
        }

        public void Delete(Account account)
        {
            _repository.delete(account);
            var transactionQuery=_transactionClassRepository.Get();
            foreach (var transactionClass in transactionQuery.Where(tran=>tran.AccountId==account.AccountNumber))
            {
                _transactionClassRepository.delete(transactionClass);
            }

        }

       
    }
}

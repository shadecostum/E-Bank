﻿using E_Bank.Dto;
using E_Bank.Exceptions;
using E_Bank.Models;
using E_Bank.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this._transactionService = transactionService;
        }


        //deposite

        [HttpPost("deposite")]
        public IActionResult DepositeAmount([FromBody]TransactionDto transactionDto)
        {
            var result = _transactionService.Deposite(transactionDto);

            if(result == 1)
            {
                return Ok("Transaction success");
            }

            return BadRequest("server error please try again after some time");
        }


        [HttpPost("Withdrawal")]  
        public IActionResult WithdrawAmount([FromBody]TransactionDto transactionDto)
        {
            var result = _transactionService.Withdraw(transactionDto);
            if(result == 1)
            {
                return Ok("withdrew  success");
            }
            return BadRequest("Bank Server error please try again after some time");
        }

        [HttpPost("TransferAmount")]
        public IActionResult TransferAmount([FromBody]TransferDto transferDto)
        {
            var result = _transactionService.TransferAmount(transferDto);
            if (result == 1)
            {
                return Ok("Transaction success");
            }
            return BadRequest("Bank Server error please try again after some time");
        }
        

















        [HttpGet("")]
        public IActionResult GetAll()
        {
            List<TransactionDto> transactionList = new List<TransactionDto>();

             var balance=  _transactionService.GetAll();

            if(balance.Count== 0)
            {
                return BadRequest("Not found ");
                
            }
            foreach (var transaction in balance)
            {
                var conTransaction=ModelToDto(transaction);
                transactionList.Add(conTransaction);
            }
            return Ok(transactionList);

        }

       
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var transactionData = _transactionService.GetById(id);

            if (transactionData != null)
            {
                return Ok(transactionData);
            }
            throw new UserNotFoundException("Cannot find the match id");
        }



        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var matched = _transactionService.GetById(id);
            if (matched != null)
            {
                _transactionService.Delete(matched);
                return Ok(matched);
            }
            return BadRequest("No match found to delete");
        }



        private TransactionClass ConvertoModel(TransactionDto transaction)
        {
            return new TransactionClass()
            {
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                TransactionAmount = transaction.TransactionAmount,
                Description = transaction.Description,
                State = "sucsess",
                IsActive = true,
                TransactionDate = DateTime.Now,
                AccountId = transaction.AccountId,

            };
        }


        private TransactionDto ModelToDto(TransactionClass transaction)
        {
            return new TransactionDto()
            {
                AccountId = transaction.AccountId,
                TransactionType = transaction.TransactionType,
                TransactionAmount = transaction.TransactionAmount,
                Description = transaction.Description,
              //  State = transaction.State,
              //  IsActive = transaction.IsActive,
               // TransactionDate = transaction.TransactionDate,
                TransactionId = transaction.TransactionId,
            };
        }



        //[HttpPost("")]
        //public IActionResult Post(TransactionDto transactionDto) 
        //{
        //    var transactionss = ConvertoModel(transactionDto);

        //    var status = _transactionService.Add(transactionss);

        //    if(status != null)
        //    {
        //        return Ok(status);
        //    }
        //    return BadRequest("cannot added");
        //}

        //[HttpPut("")]
        //public IActionResult Put(TransactionDto transactionDto)
        //{
        //    var match=_transactionService.GetById(transactionDto.TransactionId);

        //    if(match != null)
        //    {
        //        var modified = ConvertoModel(transactionDto);
        //       var updateTransaction= _transactionService.Update(modified);
        //        return Ok(updateTransaction);
        //    }
        //    return BadRequest("Not matched ");
        //}

    }
}

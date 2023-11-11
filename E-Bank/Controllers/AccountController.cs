using E_Bank.Dto;
using E_Bank.Exceptions;
using E_Bank.Models;
using E_Bank.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        private AccountDto ModelToDto(Account account)
        {
            return new AccountDto()
            {
                AccountNumber = account.AccountNumber,
                AccountBalance=account.AccountBalance,
                AccountType = account.AccountType,
                IntrestRate = account.IntrestRate,
                IsActive = account.IsActive,
                OpenningDate = account.OpenningDate,
                CustomerId = account.CustomerId,
                TransactionsCount = account.Transactions !=null ? account.Transactions.Count() :0
                
            };
        }
       
        [HttpGet("activeAccounts")]//admin view all active account
        public IActionResult GetAll()
        {
            List<AccountDto> result = new List<AccountDto>();
            var DataList = _accountService.GetAll();

            if (DataList.Count == 0)
            {
                return BadRequest("No customer Added");
            }
            foreach (var Data in DataList)
            {
                result.Add(ModelToDto(Data));
            }
            return Ok(result);
        }


        [HttpGet("accounRequest")] //admin view all Not active account
        public IActionResult GetRequest()
        {
            //List<AccountDto> result = new List<AccountDto>();
            var DataList = _accountService.GetAllRequest();

            if (DataList.Count == 0)
            {
                return BadRequest("No Request Added");
            }
            //foreach (var Data in DataList)
            //{
            //    result.Add(ModelToDto(Data));
            //}
            return Ok(DataList);
        }







        [HttpGet("activeId/{id:int}")]
        public IActionResult ActivateAccount(int id)
        {
          var result= _accountService.ActivateRequest(id);
            if (result != null)
            {
              return  Ok("Activated success full");
            }
            return BadRequest("Cannot Activate match not found");

        }

        //[HttpGet("TransactionFilter/{id:int}")] //admin search using id get specific customer 
        //public IActionResult Get(int id)
        //{
        //    var matched = _accountService.GetById(id);

        //    if (matched != null)
        //    {
        //        return Ok(matched);
        //    }
        //    throw new UserNotFoundException("Cannot find the match id");

        //}







        [HttpGet("{id:int}")] //admin search using id get specific customer 
        public IActionResult Get(int id)
        {
            var matched = _accountService.GetById(id);

            if (matched != null)
            {
                return Ok(matched);
            }
            throw new UserNotFoundException("Cannot find the match id");

        }

        private Account DtoToModel(AccountDto account)
        {
            return new Account()
            {
                AccountNumber = account.AccountNumber,
                AccountBalance = account.AccountBalance,
                AccountType = account.AccountType,
                IntrestRate = account.IntrestRate,
                IsActive = account.IsActive=false,
                OpenningDate = account.OpenningDate,
                CustomerId = account.CustomerId,
                

            };
        }



        [HttpPost("")]
        public IActionResult Post(AccountDto accountDto)
        {
            var Converted=DtoToModel(accountDto);
          var sucess=  _accountService.Add(Converted);
            if(sucess !=null)
            {
              return  Ok(sucess);
            }
            return BadRequest("Adding error");
        }

        [HttpPut("")]
        public IActionResult Put(AccountDto accountDto)
        {
            var matched=_accountService.GetById(accountDto.AccountNumber);
            if (matched != null)
            {
                var account = DtoToModel(accountDto);
                _accountService.Update(account);
                return Ok(accountDto);
            }
            return NotFound("update data doesn't match");
        }

        [HttpDelete("")]
        public IActionResult Delete(int id)
        {
            var matched=_accountService.GetById(id);
            if (matched != null)
            {
                _accountService.Delete(matched);
                return Ok(matched);
            }
            return NotFound("Cannot find deleting item");
        }

       


    }
}

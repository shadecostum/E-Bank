using E_Bank.Dto;
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
       
        [HttpGet("")]
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



        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
          var matched= _accountService.GetById(id);

            if (matched != null)
            {
              return  Ok(matched);
            }
            return NotFound("sorry id cannot find");

        }

        private Account DtoToModel(AccountDto account)
        {
            return new Account()
            {
                AccountNumber = account.AccountNumber,
                AccountBalance = account.AccountBalance,
                AccountType = account.AccountType,
                IntrestRate = account.IntrestRate,
                IsActive = account.IsActive,
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

using E_Bank.Dto;
using E_Bank.Models;
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

        //account Details



        
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
          var matched= _accountService.GetById(id);

            if (matched != null)
            {
                Ok(matched);
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



        [HttpPost]
        public IActionResult Post(AccountDto accountDto)
        {
            var Converted=DtoToModel(accountDto);
          var sucess=  _accountService.Add(Converted);
            if(sucess !=null)
            {
                Ok(sucess);
            }
            return BadRequest("Adding error");
        }

    }
}

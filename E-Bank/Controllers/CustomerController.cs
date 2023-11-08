using E_Bank.Dto;
using E_Bank.Models;
using E_Bank.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;


        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        private CustomerDto ModelToDto(Customer customer)
        {
            return new CustomerDto()
            {
               CustomerId = customer.CustomerId,
               FirstName = customer.FirstName,
               LastName = customer.LastName,
               Email = customer.Email,
               IsActive = customer.IsActive,
               CountAccounts = customer.Accounts!=null? customer.Accounts.Count():0,
               UserId = customer.UserId
              
            };
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            List<CustomerDto> result = new List<CustomerDto>();
          var DataList=  _customerService.GetAll();

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
            var CustomerData = _customerService.GetById(id);

            if(CustomerData != null)
            {
                return Ok(CustomerData);
            }
            return BadRequest("Your search Id NotFound");
        }


        private Customer ConvertoModel(CustomerDto customerDto)
        {
            return new Customer()
            {
                CustomerId=customerDto.CustomerId,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                
                Email = customerDto.Email,
                IsActive = customerDto.IsActive = true,
                UserId= customerDto.UserId,

            };
        }

        [HttpPost("")]
        public IActionResult Post(CustomerDto customerDto) 
        {
            var customer = ConvertoModel(customerDto);
            var status= _customerService.Add(customer);

            if(status !=null) 
            {
                return Ok(status);
            }
            return BadRequest("cannot added");
        }

        [HttpPut]

        public IActionResult Put(CustomerDto customerDto)
        {
            var Customer=_customerService.GetById(customerDto.CustomerId);

            if(Customer != null)
            {
                var modified=ConvertoModel(customerDto);
                _customerService.Update(modified);
                return Ok(modified);
            }
            return BadRequest("Cannot modify data not found");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
           var matched= _customerService.GetById(id);
            if(matched != null)
            {
                _customerService.Delete(matched);
                return Ok(matched);
            }
            return BadRequest("cannot find id to delete");
        }

    }
}

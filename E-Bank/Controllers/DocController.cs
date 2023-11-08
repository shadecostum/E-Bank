using E_Bank.Dto;
using E_Bank.Models;
using E_Bank.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocController : ControllerBase
    {

        private readonly IDocService _docService;


        public DocController(IDocService docService)
        {
            _docService = docService;
        }
        private DocDto ModelToDto(Documents customer)
        {
            return new DocDto()
            {
                CustomerId = customer.CustomerId,
                DocumentId = customer.DocumentId,
                DocumentData = customer.DocumentData,
                DocumentType = customer.DocumentType,
                Status = customer.Status,
                UploadDate = customer.UploadDate
                
            

            };
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            List<DocDto> result = new List<DocDto>();
            var DataList = _docService.GetAll();

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
            var CustomerData = _docService.GetById(id);

            if (CustomerData != null)
            {
                return Ok(CustomerData);
            }
            return BadRequest("Your search Id NotFound");
        }


        private Documents ConvertoModel(DocDto customerDto)
        {
            return new Documents()
            {
               UploadDate = customerDto.UploadDate,
               CustomerId = customerDto.CustomerId,
               DocumentData = customerDto.DocumentData,
               DocumentType = customerDto.DocumentType,
               Status = customerDto.Status,
                

            };
        }

        [HttpPost("")]
        public IActionResult Post(DocDto customerDto)
        {
            var customer = ConvertoModel(customerDto);
            var status = _docService.Add(customer);

            if (status != null)
            {
                return Ok(status);
            }
            return BadRequest("cannot added");
        }

        [HttpPut]

        public IActionResult Put(DocDto customerDto)
        {
            var Customer = _docService.GetById(customerDto.DocumentId);

            if (Customer != null)
            {
                var modified = ConvertoModel(customerDto);
                _docService.Update(modified);
                return Ok(modified);
            }
            return BadRequest("Cannot modify data not found");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var matched = _docService.GetById(id);
            if (matched != null)
            {
                _docService.Delete(matched);
                return Ok(matched);
            }
            return BadRequest("cannot find id to delete");
        }




    }
}

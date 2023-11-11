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
    public class QueryController : ControllerBase
    {
        private readonly IQueryService _querService;


        public QueryController(IQueryService queryService)
        {
            _querService = queryService;
        }
        private QueryDto ModelToDto(Query query)
        {
            return new QueryDto()
            {
              CustomerId = query.CustomerId,
              QueryId = query.QueryId,
              QueryStatus = query.QueryStatus,
              QueryText = query.QueryText,
              ReplyQuery = query.ReplyQuery

            };
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            List<QueryDto> result = new List<QueryDto>();
            var DataList = _querService.GetAll();

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
            var CustomerData = _querService.GetById(id);

            if (CustomerData != null)
            {
                return Ok(CustomerData);
            }
            throw new UserNotFoundException("Cannot find the match id");
        }


        private Query ConvertoModel(QueryDto queryDto)
        {
            return new Query()
            {
              
                CustomerId=queryDto.CustomerId,
                QueryId=queryDto.QueryId,
                QueryStatus = queryDto.QueryStatus,
                QueryText = queryDto.QueryText,
                ReplyQuery = queryDto.ReplyQuery
                
            };
        }

        [HttpPost("")]
        public IActionResult Post(QueryDto customerDto)
        {
            var customer = ConvertoModel(customerDto);
            var status = _querService.Add(customer);

            if (status != null)
            {
                return Ok(status);
            }
            return BadRequest("cannot added");
        }

        [HttpPut]

        public IActionResult Put(QueryDto customerDto)
        {
            var Customer = _querService.GetById(customerDto.QueryId);

            if (Customer != null)
            {
                var modified = ConvertoModel(customerDto);
                _querService.Update(modified);
                return Ok(modified);
            }
            return BadRequest("Cannot modify data not found");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var matched = _querService.GetById(id);
            if (matched != null)
            {
                _querService.Delete(matched);
                return Ok(matched);
            }
            return BadRequest("cannot find id to delete");
        }



    }
}

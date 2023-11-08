using E_Bank.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Bank.Dto
{
    public class QueryDto
    {
        public int QueryId { get; set; }

        public string QueryText { get; set; }

      

        public string ReplyQuery { get; set; }


        public string QueryStatus { get; set; }

      
        [ForeignKey("Customer")]
        public int CustomerId { get; set; } = 0;
    }
}

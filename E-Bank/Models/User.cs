using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Bank.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public Customer Customer { get; set; }
        //[ForeignKey("Customer")]
        //public int CustomerId { get; set; }


        public Role Role { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public Admin admin { get; set; }

    }
}
using E_Bank.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Bank.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

     
     //   public Role Role { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }

    }
}

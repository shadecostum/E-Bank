using E_Bank.Dto;
using E_Bank.Models;
using E_Bank.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IUserRepo _userRepo;//1
        IConfiguration _config;//a1

        public LoginController(IUserRepo userRepo, IConfiguration config)//2
        {
            _userRepo = userRepo;
            _config = config;
        }

        [HttpPost("register")]//3 register

        public IActionResult Register(UserDto userDto)
        {
            //checking existing user first registering
            var existingUser=_userRepo.FindUser(userDto.UserName);

            if (existingUser == null)
            {
                //no user found
                if(_userRepo.AddUser(userDto) != null)
                {
                    return Ok(new ReturnMessage() { Message="Registered succesfully"});

                }
                return BadRequest("some issue inserting");


            }
            return BadRequest("user alredy exist");

        }


        [HttpPost("login")]
        public IActionResult Login(UserDto userDto)
        {
            var existingUser = _userRepo.FindUser(userDto.UserName);
            if(existingUser != null)
            {
                //hashing have done
                if(BCrypt.Net.BCrypt.Verify(userDto.Password, existingUser.Password))
                {
                    var jwt = CreateToken(existingUser);


                    return Ok(new Token() { ActualToken=jwt});//for just login this much needed we need give toke so upper line
                    //now passing token data to responce logined
                }
               
            }
            return BadRequest("username/password inavlid");
        }

        private string CreateToken(User existingUser)
        {

            var role = _userRepo.GetRoleName(existingUser);

           List<Claim> claims = new List<Claim>()
            {

                new Claim(ClaimTypes.Name,existingUser.UserName),
                new Claim(ClaimTypes.Role,role)//role get by GetRoleNmae
            };

            var key=new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Key").Value));

            var cred=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var token=new JwtSecurityToken(
                
                claims:claims,
                expires:DateTime.Now.AddDays(5),
                signingCredentials: cred);

            var jwt=new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;




        }
    }
}

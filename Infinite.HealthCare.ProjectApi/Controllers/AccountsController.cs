using Infinite.HealthCare.ProjectApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
//using static Infinite.HealthCare.ProjectApi.Models.User;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static Infinite.HealthCare.ProjectApi.Models.User;
using System.Threading.Tasks;

namespace Infinite.HealthCare.ProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbcontext;
      

        public AccountsController(IConfiguration configuration, ApplicationDbContext dbcontext)
        {
            _configuration = configuration;
            _dbcontext = dbcontext;
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var currentUser = _dbcontext.Users.FirstOrDefault(x => x.Username == login.Username && x.Password == login.Password);
            if (currentUser == null)
            {
                return NotFound("InValid Username or Password");
            }
            var token = GenerateToken(currentUser);
            if (token == null)
            {
                return NotFound("Invalid credentials");
            }
            return Ok(token);
        }
        [NonAction]
        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512); var myClaims = new List<Claim>
                {
                         new Claim(ClaimTypes.Name,user.Username),
                         new Claim(ClaimTypes.Email,user.EmailId),
                         
                         new Claim(ClaimTypes.Role,user.Role ),
                 };
            var token = new JwtSecurityToken(issuer: _configuration["JWT:issuer"],
            audience: _configuration["JWT:audience"],
            claims: myClaims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials); return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet("GetName"), Authorize]
        public IActionResult GetName()
        {
            var name = User.Identity.Name;
            return Ok(name);
        }
        [HttpGet("GetRole")]
        public IActionResult GetRole()
        {
            var Role = User.FindFirstValue(ClaimTypes.Role);
            return Ok(Role);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterPatient(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            //user.Role = "Customer";
            _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("RegisterDoctor")]
        public async Task<IActionResult> RegisterDoctor(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();
            return Ok();
        }

    }
}

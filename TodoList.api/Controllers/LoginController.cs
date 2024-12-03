using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoList.api.Entities;
using TodoList.Models;

namespace TodoList.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public LoginController(IConfiguration configuration,
                               UserManager<User> userManager,
                               SignInManager<User> signInManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null) return BadRequest(new LoginResponse { Success = false, Error = "Username and password are invalid." });

            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.PassWord, false, false);

            if (!result.Succeeded) return BadRequest(new LoginResponse { Success = false, Error = "Username and password are invalid." });

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login.UserName),
                new Claim("UserId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Tokens:Issuer"],
                _configuration["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: creds
            );

            return Ok(new LoginResponse { Success = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentManagementAPI.Data;
using StudentManagementAPI.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            if (user == null)
                return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("YourSecretKey123");

            var token = new JwtSecurityToken(
                claims: new[] { new Claim("id", user.Id.ToString()) },
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            );

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
    }
}

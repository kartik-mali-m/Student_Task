using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Model;
using StudentManagementAPI.Services;

namespace StudentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var token = _service.Login(model.Username, model.Password);

            if (token == null)
                return Unauthorized("Invalid username or password");

            return Ok(new { token });
        }
    }
}
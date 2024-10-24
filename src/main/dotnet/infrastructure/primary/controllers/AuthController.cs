using Core.UseCases;
using Microsoft.AspNetCore.Mvc;
using Core.Domain.Models;
using Infrastructure.Primary.DTO;

namespace MyApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthUseCase _authUseCase;

        public AuthController(AuthUseCase authUseCase)
        {
            _authUseCase = authUseCase;
        }

        [HttpPost("sign-in")]
        public IActionResult SignIn([FromBody] SignInDto request)
        {
            try
            {
                var token = _authUseCase.SignIn(new User { Username = request.Username, Password = request.Password });
                return Ok(new { Token = token.AccessToken });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }

        [HttpPost("sign-up")]
        public IActionResult SignUp([FromBody] SignUpDto request)
        {
            try
            {
                _authUseCase.SignUp(new User { Username = request.Username, Password = request.Password });
                return Ok(new { Message = "Usuario creado exitosamente." });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { Message = ex.Message });
            }
        }
    }
}

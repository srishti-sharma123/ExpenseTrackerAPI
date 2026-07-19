using ExpenseTrackerAPI.Models.Domain;
using ExpenseTrackerAPI.Models.DTOs.Auth;
using ExpenseTrackerAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = request.Password, // temporary
                CreatedAt = DateTime.UtcNow
            };

            var createdUser = await _userRepository.CreateAsync(user);

            return Ok(new
            {
                Message = "User registered successfully",
                UserId = createdUser.Id
            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            if (user.PasswordHash != request.Password)
            {
                return Unauthorized("Invalid email or password");
            }

            return Ok("Login successful");
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Auth Controller Working");
        }
    }
}
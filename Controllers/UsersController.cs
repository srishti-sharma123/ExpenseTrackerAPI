using ExpenseTrackerAPI.Models.Domain;
using ExpenseTrackerAPI.Models.DTOs.Transaction;
using ExpenseTrackerAPI.Models.DTOs.User;
using ExpenseTrackerAPI.Repositories;
using ExpenseTrackerAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();

            var userDtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                CreatedAt = u.CreatedAt
            });

            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };

            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = request.PasswordHash,
                CreatedAt = DateTime.UtcNow
            };

            var createdUser = await _userRepository.CreateAsync(user);

            var userDto = new UserDto
            {
                Id = createdUser.Id,
                Name = createdUser.Name,
                Email = createdUser.Email,
                CreatedAt = createdUser.CreatedAt
            };

            return CreatedAtAction(
                nameof(GetUserById),
                new { id = userDto.Id },
                userDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(
    int id,
    UpdateUserRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                Id = id,
                Name = request.Name,
                Email = request.Email
            };

            var updatedUser = await _userRepository.UpdateAsync(user);

            if (updatedUser == null)
            {
                return NotFound();
            }

            var userDto = new UserDto
            {
                Id = updatedUser.Id,
                Name = updatedUser.Name,
                Email = updatedUser.Email,
                CreatedAt = updatedUser.CreatedAt
            };

            return Ok(userDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deletedUser = await _userRepository.DeleteAsync(id);

            if (deletedUser == null)
            {
                return NotFound();
            }

            var userDto = new UserDto
            {
                Id = deletedUser.Id,
                Name = deletedUser.Name,
                Email = deletedUser.Email,
                CreatedAt = deletedUser.CreatedAt
            };

            return Ok(userDto);
        }

    }
}
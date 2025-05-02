using Microsoft.AspNetCore.Mvc;
using StockAdvisorBackend.DTOs;
using StockAdvisorBackend.Models;
using Microsoft.AspNetCore.Identity.Data;
using StockAdvisorBackend.Services.Interfaces;
using LoginRequest = StockAdvisorBackend.DTOs.LoginRequest;
using AdviceRequsetDto = StockAdvisorBackend.DTOs.AdviceRequsetDto;


namespace StockAdvisorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) //Dependency Injection
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AdviceRequsetDto request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Username and password are required.");

            var user = new UserModel
            {
                Username = request.Username,
                PasswordHash = request.Password // בשלב הזה אין הצפנה אמיתית (רק הדגמה פשוטה)
            };

            await _userService.AddUserAsync(user);

            return Ok(new
            {
                success = true,
                message = "User registered successfully!",
                userId = user.Id  // ודא ש־Id קיים ונוצר ב־AddUserAsync
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers() // Get all users
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Username and password are required.");

            var user = await _userService.GetUserByUserNameAsync(request.Username);

            if (user == null || user.PasswordHash != request.Password)
                return Unauthorized("Invalid username or password.");

            //return Ok(new // Return a token or user info
            //{
            //    userId = user.Id,
            //    message = $"Welcome back, {user.Username}!"
            //});

            return Ok(new
            {
                id = user.Id,                      // ✅ זו השורה שחשובה!
                username = user.Username,
                message = "Login successful!"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto request)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("User not found.");

            user.Username = request.Username;
            user.PasswordHash = request.Password; // תלוי אם יש הצפנה בעתיד
                                                  // תוסיף פה עוד שדות אם תרצה בעתיד (אימייל, טלפון וכו')

            await _userService.UpdateUserAsync(user);

            return Ok("User updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("User not found.");

            await _userService.DeleteUserAsync(id);

            return Ok("User deleted successfully!");
        }
    }
}

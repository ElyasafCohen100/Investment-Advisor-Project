// ╔═════════════════════════════════════════════════════════════════════╗
// ║                      👤 UserController.cs                             
// ║                                                                           
// ║  💡 Purpose:                                                             
// ║     Manages user actions:                                                
// ║     - Register new users                                                 
// ║     - Login with simple credentials                                      
// ║     - Get user info (all or by ID)                                       
// ║     - Update or delete user account                                      
// ║                                                                           
// ║  🧰 Note:                                                                
// ║     Passwords are stored in plain text here (for demo only!).            
// ║     In production, use hashing & encryption!                             
// ╚═════════════════════════════════════════════════════════════════════╝

using Microsoft.AspNetCore.Mvc;
using StockAdvisorBackend.DTOs;
using StockAdvisorBackend.Models;
using StockAdvisorBackend.Services.Interfaces;
using LoginRequest = StockAdvisorBackend.DTOs.LoginRequest;
using AdviceRequsetDto = StockAdvisorBackend.DTOs.AdviceRequsetDto;

namespace StockAdvisorBackend.Controllers
{
    // ======= Route: api/User ======= //
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // ======= Injected user service ======= //
        private readonly IUserService _userService;

        // ======= Constructor ======= //
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // ======= POST: Register a new user ======= //
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AdviceRequsetDto request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Username and password are required.");

            var user = new UserModel
            {
                Username = request.Username,
                PasswordHash = request.Password // 🔓 Plain text! Hash it in real apps!
            };

            await _userService.AddUserAsync(user);

            return Ok(new
            {
                success = true,
                message = "User registered successfully!",
                userId = user.Id
            });
        }

        // ======= GET: Get all users ======= //
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // ======= POST: User login ======= //
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Username and password are required.");

            var user = await _userService.GetUserByUserNameAsync(request.Username);

            if (user == null || user.PasswordHash != request.Password)
                return Unauthorized("Invalid username or password.");

            return Ok(new
            {
                userId = user.Id,
                username = user.Username,
                message = "Login successful!"
            });
        }

        // ======= GET: Get user by ID ======= //
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        // ======= PUT: Update user details ======= //
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto request)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("User not found.");

            user.Username = request.Username;
            user.PasswordHash = request.Password;

            await _userService.UpdateUserAsync(user);

            return Ok("User updated successfully!");
        }

        // ======= DELETE: Delete user by ID ======= //
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

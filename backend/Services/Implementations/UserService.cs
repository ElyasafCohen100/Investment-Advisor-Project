// ╔════════════════════════════════════════════════════════════════════════════╗
// ║                       👤 UserService.cs                                    
// ║                                                                           
// ║ 💡 Purpose:                                                                
// ║   - Handles user-related logic such as registration, lookup, and updates. 
// ║   - Delegates data operations to IUserRepository.                         
// ║                                                                           
// ║ 🧰 Used by: UserController                                                  
// ╚════════════════════════════════════════════════════════════════════════════╝

using StockAdvisorBackend.Models;
using StockAdvisorBackend.Services.Interfaces;
using StockAdvisorBackend.Repositories.Interfaces;

namespace StockAdvisorBackend.Services.Implementations
{
    public class UserService : IUserService
    {
        // ======= Injected user repository ======= //
        private readonly IUserRepository _userRepository;

        // ======= Constructor ======= //
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // ======= Add a new user ======= //
        public async Task AddUserAsync(UserModel user)
        {
            await _userRepository.AddUserAsync(user);
        }

        // ======= Get user by username ======= //
        public async Task<UserModel> GetUserByUserNameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        // ======= Get user by ID ======= //
        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        // ======= Get all users ======= //
        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        // ======= Update user details ======= //
        public async Task UpdateUserAsync(UserModel user)
        {
            await _userRepository.UpdateUserAsync(user);
        }

        // ======= Delete user by ID ======= //
        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteUserAsync(id);
        }
    }
}

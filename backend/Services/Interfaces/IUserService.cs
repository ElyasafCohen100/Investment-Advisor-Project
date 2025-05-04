// ╔═══════════════════════════════════════════════════════════════════════════╗
// ║                         👤 IUserService.cs                                          
// ║                                                                                    
// ║ 💡 Purpose:                                                                         
// ║   - Interface defining the user-related business logic layer.                      
// ║   - Includes methods for user creation, update, deletion, and lookup.              
// ║                                                                                    
// ║ 📦 Implemented by: UserService                                                      
// ╚═══════════════════════════════════════════════════════════════════════════╝

using StockAdvisorBackend.Models;

namespace StockAdvisorBackend.Services.Interfaces
{
    public interface IUserService
    {
        Task DeleteUserAsync(int id);
        Task AddUserAsync(UserModel user);
        Task UpdateUserAsync(UserModel user);
        Task<UserModel> GetUserByIdAsync(int id);
        Task<List<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByUserNameAsync(string username);
    }
}

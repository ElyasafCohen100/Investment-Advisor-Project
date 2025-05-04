// ╔══════════════════════════════════════════════════════════════════╗
// ║                 👤 IUserRepository Interface                           
// ║  Defines the contract for managing user data in the database            
// ║  Includes methods to create, read, update, and delete users               
// ╚══════════════════════════════════════════════════════════════════╝

using StockAdvisorBackend.Models;

namespace StockAdvisorBackend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task DeleteUserAsync(int id);
        Task AddUserAsync(UserModel user);
        Task UpdateUserAsync(UserModel user);
        Task<UserModel> GetUserByIdAsync(int id);
        Task<List<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByUsernameAsync(string username);
    }
}

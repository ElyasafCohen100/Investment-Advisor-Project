using System.Threading.Tasks;
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

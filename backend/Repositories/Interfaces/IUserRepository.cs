using StockAdvisorBackend.Models;
using System.Threading.Tasks;

namespace StockAdvisorBackend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserByIdAsync(int id);
        Task<UserModel> GetUserByUsernameAsync(string username);
        Task AddUserAsync(UserModel user);

        Task<List<UserModel>> GetAllUsersAsync();

        Task UpdateUserAsync(UserModel user);

        Task DeleteUserAsync(int id);



    }
}

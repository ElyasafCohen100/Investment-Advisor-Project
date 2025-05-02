using System.Threading.Tasks;
using StockAdvisorBackend.Models;
using StockAdvisorBackend.Services.Interfaces;
using StockAdvisorBackend.Repositories.Interfaces;

namespace StockAdvisorBackend.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddUserAsync(UserModel user)
        {
            await _userRepository.AddUserAsync(user);
        }

        public async Task<UserModel> GetUserByUserNameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }


        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task UpdateUserAsync(UserModel user)
        {
            await _userRepository.UpdateUserAsync(user);
        }
        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteUserAsync(id);
        }
    }
}

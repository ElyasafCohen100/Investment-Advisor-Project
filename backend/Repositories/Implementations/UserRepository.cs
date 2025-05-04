// ╔════════════════════════════════════════════════════════════════════════════════╗
// ║                          👤 UserRepository.cs
// ║
// ║  💡 Implements: IUserRepository                                               
// ║                                                                                
// ║  ✅ Purpose:                                                                  
// ║     Handles all user-related operations in the database — such as creating,   
// ║     reading, updating, and deleting users. Supports lookup by ID or username. 
// ║                                                                               
// ║  🧰 Tech:                                                                      
// ║     - Entity Framework Core (async database access)                           
// ╚════════════════════════════════════════════════════════════════════════════════╝

using StockAdvisorBackend.Data;
using StockAdvisorBackend.Models;
using Microsoft.EntityFrameworkCore;
using StockAdvisorBackend.Repositories.Interfaces;


namespace StockAdvisorBackend.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        // ======= Constructor with DB context ======= //
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // ======= Get a user by their ID ======= //
        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        // ======= Get a user by their username ======= //
        public async Task<UserModel> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                                 .FirstOrDefaultAsync(u => u.Username == username);
        }

        // ======= Add a new user ======= //
        public async Task AddUserAsync(UserModel user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // ======= Get all users ======= //
        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // ======= Update an existing user ======= //
        public async Task UpdateUserAsync(UserModel user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        // ======= Delete a user by ID ======= //
        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}

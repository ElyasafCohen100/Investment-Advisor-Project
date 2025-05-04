// ╔════════════════════════════════════════════════════════════════════════════╗
// ║                          🗄️ ApplicationDbContext                                
// ║  Acts as the main gateway to the database using Entity Framework Core      
// ║  Contains DbSet<> for each entity (each one becomes a DB table)            
// ║  Handles communication with Somee SQL database through EF configuration    
// ╚════════════════════════════════════════════════════════════════════════════╝

using Microsoft.EntityFrameworkCore;
using StockAdvisorBackend.Models;

namespace StockAdvisorBackend.Data // 📂 Data Access Layer (DAL)
{
    public class ApplicationDbContext : DbContext
    {
        // ╔══════════════════════════════════════════════════════════╗
        // ║        📌 Constructor: Inject DB context options         
        // ║ Used by ASP.NET Core to configure the database provider  
        // ╚══════════════════════════════════════════════════════════╝
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ╔══════════════════════════════════════════════════════════════════╗
        // ║                          📊 Database Tables                       
        // ║ Each DbSet<T> below becomes a table in the SQL Server database.    
        // ╚══════════════════════════════════════════════════════════════════╝

        public DbSet<UserModel> Users { get; set; }                        // 🧑 Users table
        public DbSet<StockModel> Stocks { get; set; }                      // 📈 Stocks table
        public DbSet<PortfolioModel> PortfolioItems { get; set; }          // 💼 Portfolio holdings
        public DbSet<TransactionModel> Transactions { get; set; }          // 💸 Stock transactions
        public DbSet<AdviceRequestModel> AdviceRequests { get; set; }      // 🤖 User advice/chat history
        public DbSet<EventModel> Events { get; set; }                      // 📋 Event logs for auditing
    }
}

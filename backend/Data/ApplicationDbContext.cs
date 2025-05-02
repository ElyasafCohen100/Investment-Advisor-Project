using Microsoft.EntityFrameworkCore;
using StockAdvisorBackend.Models;
using System.Collections.Generic;


//It tells Entity Framework (ORM library) which tables are in the system.

////Each DbSet<T> is a table.

////It will also manage all connections to Somee 


namespace StockAdvisorBackend.Data // Data access layer
{
    public class ApplicationDbContext : DbContext // This class represents the database context for the application.
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // טבלאות במערכת
        public DbSet<UserModel> Users { get; set; }
        public DbSet<StockModel> Stocks { get; set; }
        public DbSet<PortfolioModel> PortfolioItems { get; set; }
        public DbSet<TransactionModel> Transactions { get; set; }
        public DbSet<AdviceRequestModel> AdviceRequests { get; set; } // אם החלטת להוסיף גם ייעוץ

        public DbSet<EventModel> Events { get; set; }

    }
}

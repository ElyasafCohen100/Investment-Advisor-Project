// ╔══════════════════════════════════════════════════════════════════════════╗
// ║           📸 ApplicationDbContextModelSnapshot                          
// ║  Auto-generated snapshot file by EF Core.                                
// ║  Keeps track of the current database model state.                        
// ╚══════════════════════════════════════════════════════════════════════════╝

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockAdvisorBackend.Data;

#nullable disable

namespace StockAdvisorBackend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618

            // ========== General EF Configuration ========== //
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            // ========== AdviceRequests Table ========== //
            modelBuilder.Entity("StockAdvisorBackend.Models.AdviceRequestModel", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<DateTime>("CreatedAt").HasColumnType("datetime2");
                b.Property<string>("Question").HasColumnType("nvarchar(max)");
                b.Property<string>("Response").HasColumnType("nvarchar(max)");
                b.Property<int>("UserId").HasColumnType("int");

                b.HasKey("Id");
                b.HasIndex("UserId");
                b.ToTable("AdviceRequests");
            });

            // ========== Events Table ========== //
            modelBuilder.Entity("StockAdvisorBackend.Models.EventModel", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<int>("AggregateId").HasColumnType("int");
                b.Property<string>("AggregateType").IsRequired().HasColumnType("nvarchar(max)");
                b.Property<DateTime>("CreatedAt").HasColumnType("datetime2");
                b.Property<string>("EventData").IsRequired().HasColumnType("nvarchar(max)");
                b.Property<string>("EventType").IsRequired().HasColumnType("nvarchar(max)");

                b.HasKey("Id");
                b.ToTable("Events");
            });

            // ========== PortfolioItems Table ========== //
            modelBuilder.Entity("StockAdvisorBackend.Models.PortfolioModel", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<decimal>("AveragePurchasePrice").HasColumnType("decimal(18,2)");
                b.Property<int>("PortfolioQuantity").HasColumnType("int");
                b.Property<int>("StockId").HasColumnType("int");
                b.Property<int>("UserId").HasColumnType("int");
                b.Property<int?>("UserModelId").HasColumnType("int");

                b.HasKey("Id");
                b.HasIndex("StockId");
                b.HasIndex("UserModelId");
                b.ToTable("PortfolioItems");
            });

            // ========== Stocks Table ========== //
            modelBuilder.Entity("StockAdvisorBackend.Models.StockModel", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<decimal>("CurrentPrice").HasColumnType("decimal(18,2)");
                b.Property<DateTime>("LastUpdated").HasColumnType("datetime2");
                b.Property<string>("Symbol").IsRequired().HasColumnType("nvarchar(max)");

                b.HasKey("Id");
                b.ToTable("Stocks");
            });

            // ========== Transactions Table ========== //
            modelBuilder.Entity("StockAdvisorBackend.Models.TransactionModel", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<decimal>("PriceAtTransaction").HasColumnType("decimal(18,2)");
                b.Property<int>("StockId").HasColumnType("int");
                b.Property<int>("TransactionAmount").HasColumnType("int");
                b.Property<DateTime>("TransactionDate").HasColumnType("datetime2");
                b.Property<string>("TransactionType").IsRequired().HasColumnType("nvarchar(max)");
                b.Property<int>("UserId").HasColumnType("int");
                b.Property<int?>("UserModelId").HasColumnType("int");

                b.HasKey("Id");
                b.HasIndex("StockId");
                b.HasIndex("UserModelId");
                b.ToTable("Transactions");
            });

            // ========== Users Table ========== //
            modelBuilder.Entity("StockAdvisorBackend.Models.UserModel", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("PasswordHash").IsRequired().HasColumnType("nvarchar(max)");
                b.Property<string>("Username").IsRequired().HasColumnType("nvarchar(max)");

                b.HasKey("Id");
                b.ToTable("Users");
            });

            // ========== Relationships ========== //
            modelBuilder.Entity("StockAdvisorBackend.Models.AdviceRequestModel", b =>
            {
                b.HasOne("StockAdvisorBackend.Models.UserModel", "User")
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("User");
            });

            modelBuilder.Entity("StockAdvisorBackend.Models.PortfolioModel", b =>
            {
                b.HasOne("StockAdvisorBackend.Models.StockModel", "Stock")
                    .WithMany()
                    .HasForeignKey("StockId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("StockAdvisorBackend.Models.UserModel", null)
                    .WithMany("Portfolio")
                    .HasForeignKey("UserModelId");

                b.Navigation("Stock");
            });

            modelBuilder.Entity("StockAdvisorBackend.Models.TransactionModel", b =>
            {
                b.HasOne("StockAdvisorBackend.Models.StockModel", "Stock")
                    .WithMany()
                    .HasForeignKey("StockId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("StockAdvisorBackend.Models.UserModel", null)
                    .WithMany("Transactions")
                    .HasForeignKey("UserModelId");

                b.Navigation("Stock");
            });

            modelBuilder.Entity("StockAdvisorBackend.Models.UserModel", b =>
            {
                b.Navigation("Portfolio");
                b.Navigation("Transactions");
            });

#pragma warning restore 612, 618
        }
    }
}

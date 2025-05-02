using StockAdvisorBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


//A Repository is a file that contains functions that talk to the database: saving, retrieving, updating, and deleting objects from the DB.
// It is a design pattern that separates the logic that retrieves data from the underlying storage system.
namespace StockAdvisorBackend.Repositories.Interfaces
{
    public interface IStockRepository // Interface for stock repository

        /// This interface defines the contract for stock-related data operations.
    {
        Task<StockModel> GetStockByIdAsync(int id);
        Task<List<StockModel>> GetAllStocksAsync();
        Task AddStockAsync(StockModel stock);
        Task UpdateStockAsync(StockModel stock);
        Task DeleteStockAsync(StockModel stock);
        Task<StockModel> GetStockBySymbolAsync(string symbol);

    }
}

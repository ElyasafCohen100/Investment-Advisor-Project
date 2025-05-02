using System.Transactions;

namespace StockAdvisorBackend.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }


        public string PasswordHash { get; set; }

        public List<PortfolioModel> Portfolio { get; set; }
        public List<TransactionModel> Transactions { get; set; }
    }
}

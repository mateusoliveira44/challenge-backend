using challenge_backend.Domain.Entities;

namespace challenge_backend.Application.Ports.Services.Interfaces
{
    public interface IWalletService
    {
        Task<decimal> GetBalance(int userId);
        Task Deposit(int userId, decimal amount);
        Task Transfer(int fromUserId, int toUserId, decimal amount);
        Task<List<Transaction>> GetTransactions(int userId, DateTime? startDate, DateTime? endDate);
    }
}

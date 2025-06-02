using challenge_backend.Core;
using challenge_backend.Domain.Aggregates;

namespace challenge_backend.Application.Ports.Repositories
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        Task<Wallet> GetByUserId(int userId);
        void Add(Wallet wallet);
    }
}

using challenge_backend.Application.Ports.Repositories;
using challenge_backend.Core;
using challenge_backend.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace challenge_backend.PostgreSQL.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly PostgresDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public WalletRepository(PostgresDbContext context)
        {
            _context = context;
        }

        public void Add(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
        }

        public async Task<Wallet> GetByUserId(int userId)
        {
            return await _context.Wallets
                .Include(w => w.Transactions)
                .FirstOrDefaultAsync(w => w.UserId == userId);
        }
    }
}
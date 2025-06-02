using challenge_backend.Application.Ports.Repositories;
using challenge_backend.Application.Ports.Services.Interfaces;
using challenge_backend.Core;
using challenge_backend.Domain.Aggregates;
using challenge_backend.Domain.Entities;

namespace challenge_backend.Application.Ports.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;

        public WalletService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task Deposit(int userId, decimal amount)
        {
            var wallet = await _walletRepository.GetByUserId(userId);
            if (wallet == null)
            {
                wallet = new Wallet(userId);
                _walletRepository.Add(wallet);
            }

            wallet.AddBalance(amount);
            await _walletRepository.UnitOfWork.CommitAsync();
        }

        public async Task<decimal> GetBalance(int userId)
        {
            var wallet = await _walletRepository.GetByUserId(userId);
            if (wallet == null)
                throw new DomainException("Carteira não encontrada.");

            return wallet.Balance;
        }

        public async Task<List<Transaction>> GetTransactions(int userId, DateTime? startDate, DateTime? endDate)
        {
            var wallet = await _walletRepository.GetByUserId(userId);
            if (wallet == null)
                throw new DomainException("Carteira não encontrada.");

            var transactions = wallet.Transactions.AsQueryable();

            if (startDate.HasValue)
                transactions = transactions.Where(t => t.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                transactions = transactions.Where(t => t.CreatedAt <= endDate.Value);

            return transactions.OrderByDescending(t => t.CreatedAt).ToList();
        }

        public async Task Transfer(int fromUserId, int toUserId, decimal amount)
        {
            if (fromUserId == toUserId)
                throw new DomainException("Não é possível transferir para a própria conta.");

            var fromWallet = await _walletRepository.GetByUserId(fromUserId);
            if (fromWallet == null)
                throw new DomainException("Carteira de origem não encontrada.");

            var toWallet = await _walletRepository.GetByUserId(toUserId);
            if (toWallet == null)
            {
                toWallet = new Wallet(toUserId);
                _walletRepository.Add(toWallet);
            }

            fromWallet.TransferTo(toUserId, amount);
            toWallet.ReceiveTransfer(fromUserId, amount);

            await _walletRepository.UnitOfWork.CommitAsync();
        }
    }
}

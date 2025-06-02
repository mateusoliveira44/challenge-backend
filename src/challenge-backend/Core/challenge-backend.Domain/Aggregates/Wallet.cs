using challenge_backend.Core;
using challenge_backend.Domain.Entities;
using challenge_backend.Domain.Enums;

namespace challenge_backend.Domain.Aggregates
{
    public class Wallet : Entity, IAggregateRoot
    {
        public int UserId { get; private set; }
        public decimal Balance { get; private set; }

        private readonly List<Transaction> _transactions = new();
        public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

        private Wallet() { }

        public Wallet(int userId)
        {
            UserId = userId;
            Balance = 0;
        }

        public void AddBalance(decimal amount)
        {
            if (amount <= 0)
                throw new DomainException("O valor deve ser maior que zero");

            Balance += amount;
            _transactions.Add(new Transaction(null, UserId, amount, TransactionType.Deposit));
        }

        public void TransferTo(int destinationUserId, decimal amount)
        {
            if (amount <= 0)
                throw new DomainException("O valor deve ser maior que zero");

            if (Balance < amount)
                throw new DomainException("Saldo insuficiente");

            Balance -= amount;
            _transactions.Add(new Transaction(UserId, destinationUserId, amount, TransactionType.Transfer));
        }

        public void ReceiveTransfer(int sourceUserId, decimal amount)
        {
            if (amount <= 0)
                throw new DomainException("O valor deve ser maior que zero");

            Balance += amount;
            _transactions.Add(new Transaction(sourceUserId, UserId, amount, TransactionType.Transfer));
        }
    }
}

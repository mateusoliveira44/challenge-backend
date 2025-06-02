using challenge_backend.Core;
using challenge_backend.Domain.Enums;

namespace challenge_backend.Domain.Entities
{
    public class Transaction : Entity
    {
        public int? SourceUserId { get; private set; }
        public int DestinationUserId { get; private set; }
        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Transaction() { }

        public Transaction(int? sourceUserId, int destinationUserId, decimal amount, TransactionType type)
        {
            SourceUserId = sourceUserId;
            DestinationUserId = destinationUserId;
            Amount = amount;
            Type = type;
            CreatedAt = DateTime.UtcNow;
        }
    }
}

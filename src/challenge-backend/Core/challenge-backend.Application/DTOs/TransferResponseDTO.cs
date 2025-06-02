using challenge_backend.Domain.Enums;

namespace challenge_backend.Application.DTOs
{
    public class TransferResponseDTO
    {
        public TransferResponseDTO(int? fromUserId, int toUserId, decimal amount, TransactionType type, DateTime date)
        {
            FromUserId = fromUserId;
            ToUserId = toUserId;
            Amount = amount;
            Type = type;
            Date = date;
        }

        public int? FromUserId { get; set; }
        public int ToUserId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
    }
}
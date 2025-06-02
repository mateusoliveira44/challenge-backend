
namespace challenge_backend.Application.DTOs
{
    public class TransferRequestDTO
    {
        public int SourceUserId { get; set; }
        public int ToUserId { get; set; }
        public decimal Amount { get; set; }
    }
}
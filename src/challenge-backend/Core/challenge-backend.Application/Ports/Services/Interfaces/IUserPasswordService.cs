using challenge_backend.Domain.Aggregates;

namespace challenge_backend.Application.Ports.Services.Interfaces
{
    public interface IUserPasswordService
    {
        void SetUserPassword(User user, string password);
        bool VerifyPassword(User user, string password);
    }
}

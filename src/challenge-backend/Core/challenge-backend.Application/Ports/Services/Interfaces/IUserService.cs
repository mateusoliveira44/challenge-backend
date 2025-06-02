using challenge_backend.Domain.Aggregates;

namespace challenge_backend.Application.Ports.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> UserRegister(string name, string email, string password);
    }
}

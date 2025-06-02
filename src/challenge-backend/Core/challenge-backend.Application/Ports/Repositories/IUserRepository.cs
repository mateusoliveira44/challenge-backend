using challenge_backend.Core;
using challenge_backend.Domain.Aggregates;

namespace challenge_backend.Application.Ports.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> Register(User user);
        Task<User> GetByEmail(string email);
    }
}

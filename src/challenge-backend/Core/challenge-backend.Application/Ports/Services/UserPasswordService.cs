using challenge_backend.Application.Ports.Repositories;
using challenge_backend.Application.Ports.Services.Interfaces;
using challenge_backend.Domain.Aggregates;

namespace challenge_backend.Application.Ports.Services
{
    public class UserPasswordService : IUserPasswordService
    {
        private readonly IPasswordHasher _passwordHasher;

        public UserPasswordService(IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public void SetUserPassword(User user, string password)
        {
            var hash = _passwordHasher.HashPassword(password);
            user.SetPassword(hash);
        }

        public bool VerifyPassword(User user, string password)
        {
            return _passwordHasher.Verify(password, user.PasswordHash);
        }
    }
}

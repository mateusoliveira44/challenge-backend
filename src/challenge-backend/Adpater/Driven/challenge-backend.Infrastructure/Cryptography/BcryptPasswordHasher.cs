using challenge_backend.Application.Ports.Repositories;

namespace challenge_backend.Infrastructure.Cryptography
{
    public class BcryptPasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("A senha não pode ser nula ou vazia.");
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
                return false;
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}

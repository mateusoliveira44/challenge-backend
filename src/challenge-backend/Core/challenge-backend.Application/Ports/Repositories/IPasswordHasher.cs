namespace challenge_backend.Application.Ports.Repositories
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool Verify(string password, string hashedPassword);
    }
}

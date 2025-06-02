namespace challenge_backend.Application.Ports.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(string email, string password);
    }
}

using challenge_backend.Application.Ports.Repositories;
using challenge_backend.Application.Ports.Services.Interfaces;
using challenge_backend.Domain.Aggregates;

namespace challenge_backend.Application.Ports.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserPasswordService _passwordService;

        public UserService(IUserRepository userRepository, IUserPasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public async Task<User> UserRegister(string name, string email, string password)
        {
            var user = new User(name, email);
            _passwordService.SetUserPassword(user, password);

            var newUser = await _userRepository.Register(user);
            await _userRepository.UnitOfWork.CommitAsync();

            return newUser;
        }
    }
}

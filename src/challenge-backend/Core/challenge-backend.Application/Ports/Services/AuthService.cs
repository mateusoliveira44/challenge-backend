using challenge_backend.Application.Options;
using challenge_backend.Application.Ports.Repositories;
using challenge_backend.Application.Ports.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace challenge_backend.Application.Ports.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserPasswordService _passwordService;
        private readonly AuthenticationOptions _authenticationOptions;

        public AuthService(IUserRepository userRepository, IUserPasswordService passwordService, IOptions<AuthenticationOptions> authenticationOptions)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _authenticationOptions = authenticationOptions.Value;
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user == null)
                throw new UnauthorizedAccessException("Usuário ou senha inválidos");

            if (!_passwordService.VerifyPassword(user, password))
                throw new UnauthorizedAccessException("Usuário ou senha inválidos");

            var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationOptions.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

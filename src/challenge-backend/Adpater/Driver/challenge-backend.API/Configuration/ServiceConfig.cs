using challenge_backend.Application.Ports.Services.Interfaces;
using challenge_backend.Application.Ports.Services;
using challenge_backend.Infrastructure.Cryptography;
using challenge_backend.Application.Ports.Repositories;

namespace challenge_backend.API.Configuration
{
    public static class ServiceConfig
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserPasswordService, UserPasswordService>();
            services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();

            return services;
        }
    }
}
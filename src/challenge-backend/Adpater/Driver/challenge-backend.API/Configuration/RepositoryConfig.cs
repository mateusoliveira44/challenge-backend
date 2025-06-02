using challenge_backend.PostgreSQL.Repositories;
using challenge_backend.Application.Ports.Repositories;

namespace challenge_backend.API.Configuration
{
    public static class RepositoryConfig
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();

            return services;
        }
    }
}
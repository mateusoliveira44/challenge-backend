using challenge_backend.Application.Ports.Repositories;
using challenge_backend.Core;
using challenge_backend.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace challenge_backend.PostgreSQL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PostgresDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public UserRepository(PostgresDbContext context)
        {
            _context = context;
        }

        public async Task<User> Register(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email.MailAddress == email);
        }
    }
}
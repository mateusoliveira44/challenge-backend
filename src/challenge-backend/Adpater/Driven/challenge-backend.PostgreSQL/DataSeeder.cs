using challenge_backend.Application.Ports.Services.Interfaces;
using challenge_backend.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace challenge_backend.PostgreSQL
{
    public static class DataSeeder
    {
        public static void Seed(PostgresDbContext context, IUserPasswordService passwordService)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
                SeedUsers(context, passwordService);

            if (!context.Wallets.Any())
                SeedWallets(context);

            if (!context.Transactions.Any())
                SeedTransactions(context);
        }

        private static void SeedUsers(PostgresDbContext context, IUserPasswordService passwordService)
        {
            var users = new List<User>
            {
                new User("Alice Johnson", "alice@example.com"),
                new User("Bob Smith", "bob@example.com"),
                new User("Charlie Brown", "charlie@example.com")
            };

            foreach (var user in users)
            {
                passwordService.SetUserPassword(user, "123456");
            }

            context.Users.AddRange(users);
            context.SaveChanges();
        }

        private static void SeedWallets(PostgresDbContext context)
        {
            var users = context.Users.ToList();

            foreach (var user in users)
            {
                var wallet = new Wallet(user.Id);
                wallet.AddBalance(1000);

                context.Wallets.Add(wallet);
            }

            context.SaveChanges();
        }

        private static void SeedTransactions(PostgresDbContext context)
        {
            var alice = context.Users.FirstOrDefault(u => u.Name == "Alice Johnson");
            var bob = context.Users.FirstOrDefault(u => u.Name == "Bob Smith");

            if (alice == null || bob == null) return;

            var aliceWallet = context.Wallets.Include(w => w.Transactions).FirstOrDefault(w => w.UserId == alice.Id);
            var bobWallet = context.Wallets.Include(w => w.Transactions).FirstOrDefault(w => w.UserId == bob.Id);

            if (aliceWallet == null || bobWallet == null) return;

            aliceWallet.TransferTo(bob.Id, 200);
            bobWallet.ReceiveTransfer(alice.Id, 200);

            context.SaveChanges();
        }
    }
}

using challenge_backend.Core;
using challenge_backend.Domain.ValueObjects;

namespace challenge_backend.Domain.Aggregates
{
    public class User : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string PasswordHash { get; private set; }
        public Email Email { get; private set; }

        private User() { }

        public User(string name, string email)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrEmpty(email);

            Name = name;
            Email = new Email(email);

            Validate();
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new DomainException("Senha inválida");

            PasswordHash = password;
        }

        private void Validate()
        {
            if (Name.Length <= 1 || Name.Length > 50)
                throw new DomainException($"Nome precisa ter entre 2 e 50 caracteres");
        }
    }
}

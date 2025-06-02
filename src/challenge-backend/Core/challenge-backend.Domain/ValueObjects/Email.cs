using challenge_backend.Core;
using System.Text.RegularExpressions;

namespace challenge_backend.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        private Email()
        {

        }

        public Email(string email)
        {
            ArgumentNullException.ThrowIfNull(email);

            this.MailAddress = Validar(email) ? email : throw new DomainException($"Email inválido {email}");
        }

        public string MailAddress { get; private set; }

        public static bool Validar(string email)
        {
            if (email.Length < 6 || email.Length > 100)
                throw new DomainException($"Email precisa ter entre 6 e 100 caracteres");

            const string regex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,6}$";

            return Regex.IsMatch(email, regex);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return MailAddress;
        }
    }
}

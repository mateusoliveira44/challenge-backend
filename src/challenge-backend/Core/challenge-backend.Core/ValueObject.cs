namespace challenge_backend.Core
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object?> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = (ValueObject)obj;

            return GetEqualityComponents()
                .SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    return HashCode.Combine(current, obj?.GetHashCode() ?? 0);
                });
        }

        public static bool operator ==(ValueObject? a, ValueObject? b)
        {
            return a is null && b is null || a is not null && a.Equals(b);
        }

        public static bool operator !=(ValueObject? a, ValueObject? b)
        {
            return !(a == b);
        }
    }
}

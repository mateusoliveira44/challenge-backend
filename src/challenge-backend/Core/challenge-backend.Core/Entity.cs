namespace challenge_backend.Core
{
    public abstract class Entity
    {
        public int Id { get; private set; }

        protected Entity()
        {

        }

        protected Entity(int id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = (Entity)obj;

            return Id == other.Id;
        }

        public static bool operator ==(Entity? a, Entity? b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity? a, Entity? b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() * 41;
        }
    }
}

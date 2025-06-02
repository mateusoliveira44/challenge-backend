namespace challenge_backend.Core
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}

namespace Library.Infrastructure.Idempotency
{
    public interface IRequestManager
    {
        Task<bool> ExistAsync(Guid key);

        Task CreateRequestForCommandAsync<T>(Guid key);
    }
}
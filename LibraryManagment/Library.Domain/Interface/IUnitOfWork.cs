namespace Library.Domain.Interface
{
    public interface IUnitOfWork
    {
        Task CreateTransaction(); //start db transaction
        Task Commit(); //if all operation resolved then commit 
        Task Rollback(); //if womething wrong get all operations back
        Task Save(); //save changes
        void Dispose(); //release resources

    }
}

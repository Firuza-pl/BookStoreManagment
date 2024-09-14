using Library.Domain.Entites.BookAggregate;

namespace Library.Domain.Interface
{
    public interface IUnitOfWork
    {
        IBookRepository BookRepository { get; }  //  Holds the specific repository for books
        Task CreateTransaction(); //start db transaction
        Task Commit(); //if all operation resolved then commit 
        Task Rollback(); //if womething wrong get all operations back
        Task Save(); //save changes
        void Dispose(); //release resources

    }
}

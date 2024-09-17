using Library.Domain.Entites.BookAggregate;

namespace Library.Domain.Interface
{
    public interface IUnitOfWork
    {
        //repos
        IBookRepository BookRepository { get; }  //  Holds the specific repository for books
        IMemberRepository MemberRepository { get; } 

        //general operations
        Task CreateTransaction(); //start db transaction
        Task CommitAsync(); //if all operation resolved then commit 
        Task RollbackAsync(); //if womething wrong get all operations back
        Task<int> SaveAsync(CancellationToken cancellationToken); //save changes
        void Dispose(); //release resources

    }
}

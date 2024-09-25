using Library.Domain.Entites.BookAggregate;

namespace Library.Domain.Interface;
public interface IBorrowBookRepository : IGenericRepository<BorrowRecord>
{
}

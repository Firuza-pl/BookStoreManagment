using Library.Domain.Entites.BookAggregate;
using Library.Domain.Interface;
using Library.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace Library.Infrastructure.Repositories;
public class BorrowBookRepository : GenericRepository<BorrowRecord>, IBorrowBookRepository
{
    public BorrowBookRepository(AppDbContext _context, ILogger<BorrowBookRepository> _logger) : base(_context, _logger) { }
}

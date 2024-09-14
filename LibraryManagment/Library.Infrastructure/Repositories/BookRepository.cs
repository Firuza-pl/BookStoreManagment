using AutoMapper;
using Library.Domain.Entites.BookAggregate;
using Library.Domain.Interface;
using Library.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace Library.Infrastructure.Repositories;
public class BookRepository : GenericRepository<Book>, IBookRepository
{
    public BookRepository(AppDbContext _context, ILogger<BookRepository>_logger) : base(_context, _logger) { }

    //additional logic
}

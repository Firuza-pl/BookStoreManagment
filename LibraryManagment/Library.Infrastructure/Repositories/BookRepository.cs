using AutoMapper;
using Library.Domain.Entites.BookAggregate;
using Library.Domain.Interface;
using Library.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace Library.Infrastructure.Repositories;
public class BookRepository : GenericRepository<Book>, IBookRepository
{
    public BookRepository(AppDbContext _context, IMapper _mapper, ILogger _logger) : base(_context, _mapper, _logger) { }

    //additional logic
}

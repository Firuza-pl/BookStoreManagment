using AutoMapper;
using Library.Domain.Interface;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace Library.Infrastructure.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly IMapper _mapper;
    protected readonly ILogger _logger;

    //adding UNitofwor here

    public DbSet<T> Entity => _context.Set<T>(); //access dbset for current type

    public GenericRepository(AppDbContext context, IMapper mapper, ILogger logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
}

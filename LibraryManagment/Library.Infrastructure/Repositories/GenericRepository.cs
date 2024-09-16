using AutoMapper;
using Library.Domain.Interface;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Library.Infrastructure.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly ILogger<GenericRepository<T>> _logger;

    //adding UNitofwor here

    public DbSet<T> Entity => _context.Set<T>(); //access dbset for current type

    public GenericRepository(AppDbContext context, ILogger<GenericRepository<T>> logger) { 
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<T> AddAsync(T entity)
    {
        var result = await Entity.AddAsync(entity);
        return result.Entity;
    }

    public T Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;  //tells EF to mark entity as Modified
        return entity;
    }

    public async Task<T> GetAsync(Guid id)
    {
        var result = await Entity.FindAsync(id);
        return result;
    }

    public bool Delete(T entity)
    {
         Entity.Remove(entity); //EF knows which instance to delete //EntityState.Deleted
        return true;

    }
}

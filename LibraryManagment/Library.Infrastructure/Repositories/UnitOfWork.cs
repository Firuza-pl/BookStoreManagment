using Library.Domain.Interface;
using Library.Infrastructure.Persistence;
using Library.Shared.Kernel.Core;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Library.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    private readonly ILogger<BookRepository> _logger;
    private readonly ILogger<MemberRepository> _loggerMember;

    public UnitOfWork(AppDbContext context, IDomainEventDispatcher domainEventDispatcher,ILogger<BookRepository> logger, ILogger<MemberRepository> loggerMember)
    {
        _context = context;
        _domainEventDispatcher = domainEventDispatcher;
        _logger = logger;
        _loggerMember = loggerMember;
    }

    public IDomainEventDispatcher DomainEventDispatcher => _domainEventDispatcher; // Implement the property

    private IDbContextTransaction _dbContextTransaction;
    private bool _disposed = false;

    private IBookRepository _bookRepository;
    public IBookRepository BookRepository
    {
        get
        {
            return _bookRepository ??= new BookRepository(_context, _logger);
        }
    }

    private IMemberRepository _memberRepository;
    public IMemberRepository MemberRepository
    {
        get { return _memberRepository ?? new MemberRepository(_context, _loggerMember); }
    }


 
    public async Task CommitAsync()
    {
        try
        {
            if (_dbContextTransaction is null)
                throw new ArgumentNullException(nameof(_dbContextTransaction));

            await _context.SaveChangesAsync();
            await _dbContextTransaction.CommitAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task CreateTransaction()
    {
        try
        {
            if (_dbContextTransaction is null)
                throw new ArgumentNullException(nameof(_dbContextTransaction));

            _dbContextTransaction = await _context.Database.BeginTransactionAsync();
        }
        catch (Exception)
        {
            await RollbackAsync();  //will call automatically rollback()
            throw;
        }
        finally
        {
            if (_dbContextTransaction is { })
            {
                await _dbContextTransaction.DisposeAsync();
                _dbContextTransaction = null;
            }
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)  //ensure dispose logic only runs once
        {
            if (disposing)
            {
                _context.Dispose();
            }
            disposing = true;
        }
    }
    public void Dispose() //1st
    {
        Dispose(true);
        GC.SuppressFinalize(this); //prevent finalizer from running , becasue resources already relased.
    }

    public async Task RollbackAsync()
    {
        try
        {
            if (_dbContextTransaction is null)
                throw new ArgumentNullException(nameof(_dbContextTransaction));

            await _dbContextTransaction.RollbackAsync();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if (_dbContextTransaction is { })
            {
                await _dbContextTransaction.DisposeAsync();
                _dbContextTransaction = null;
            }
        }
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken)
    {

        var entries = _context.ChangeTracker.Entries().ToList();
        foreach (var entry in entries)
        {
            // Log the state of each entry to see what is being tracked
            Console.WriteLine($"{entry.Entity.GetType().Name}: {entry.State}");
        }


        var entitiesWithEvents = _context.ChangeTracker
            .Entries<Entity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToArray();

        // Save the database changes
        var result = await _context.SaveChangesAsync(cancellationToken);

        // Dispatch the domain events after saving changes
        foreach (var entity in entitiesWithEvents)
        {
            var domainEvents = entity.DomainEvents.ToArray();
            entity.RemoveDoaminEvent();

            foreach (var domainEvent in domainEvents)
            {
                await _domainEventDispatcher.DispatchEventsAsync(new[] { domainEvent });
            }
        }

        return result;
    }


}


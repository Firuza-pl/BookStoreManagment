using Library.Domain.Interface;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Library.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;
    private IDbContextTransaction _dbContextTransaction;
    private bool _disposed = false;
    public UnitOfWork(AppDbContext context, ILogger<UnitOfWork> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task Commit()
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
            await Rollback();  //will call automatically rollback()
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

    public async Task Rollback()
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

    public Task Save()
    {
        throw new NotImplementedException();
    }
}

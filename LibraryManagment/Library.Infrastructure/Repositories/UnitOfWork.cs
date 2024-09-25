﻿using Library.Domain.Interface;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Library.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly ILogger<BookRepository> _logger;
    private readonly ILogger<MemberRepository> _loggerMember;
    private readonly ILogger<BorrowBookRepository> _loggerBorrow;

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


    private IBorrowBookRepository _borrowBookRepository;
    public IBorrowBookRepository BorrowBookRepository
    {
        get { return _borrowBookRepository ?? new BorrowBookRepository(_context, _loggerBorrow); }
    }


    public UnitOfWork(AppDbContext context, ILogger<BookRepository> logger, ILogger<MemberRepository> loggerMember, ILogger<BorrowBookRepository> loggerRecord)
    {
        _context = context;
        _logger = logger;
        _loggerMember = loggerMember;
        _loggerBorrow = loggerRecord;
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
      return  await _context.SaveChangesAsync(cancellationToken);
    }
}

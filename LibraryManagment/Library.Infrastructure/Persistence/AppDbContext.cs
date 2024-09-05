﻿
using Library.Domain.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence;
public class AppDbContext : IdentityDbContext<ApplicationUser>  // base class used for EF for identity
{
    public AppDbContext(DbContextOptions<AppDbContext> context) : base(context)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Member> Memberships { get; set; }
    public DbSet<BorrowRecord> BorrowRecords { get; set; }
}

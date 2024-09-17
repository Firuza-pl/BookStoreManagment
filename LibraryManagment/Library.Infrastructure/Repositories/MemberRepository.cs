using Library.Domain.Entites.MemberAggregate;
using Library.Domain.Interface;
using Library.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace Library.Infrastructure.Repositories
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(AppDbContext _context, ILogger<MemberRepository> _logger) : base(_context, _logger) { }
    }
}

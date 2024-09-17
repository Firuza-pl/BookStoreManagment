using AutoMapper;
using Library.Application.ViewModel.Books;
using Library.Application.ViewModel.Members;
using Library.Infrastructure.Persistence;
using Library.SharedKernel.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Queries.Members;
public class MemberQueries : IMemberQueries
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public MemberQueries(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<GetMemberDTO>> GetActiveAsync()
    {
        var entity = await _context.Memberships.Where(x => x.IsAvailable == MemberStatus.Active).ToListAsync();

        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        var outputModel = _mapper.Map<IEnumerable<GetMemberDTO>>(entity);

        return outputModel;
    }

    public async Task<IEnumerable<GetMemberDTO>> GetAllAsync()
    {
        var entity = await _context.Memberships.Where(x => x.IsAvailable == MemberStatus.Active || x.IsAvailable == MemberStatus.Deactive).ToListAsync();

        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        var outputModel = _mapper.Map<IEnumerable<GetMemberDTO>>(entity);

        return outputModel;
    }

    public async Task<GetMemberDTO> GetByIdAsync(Guid id)
    {
        var entity = await _context.Memberships.FirstOrDefaultAsync(x => x.Id == id);
        var outputModel = _mapper.Map<GetMemberDTO>(entity);
        return outputModel;
    }
}

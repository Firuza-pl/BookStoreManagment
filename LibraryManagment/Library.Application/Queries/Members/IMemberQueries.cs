using Library.Application.ViewModel.Members;
using Library.Infrastructure.Services.Queries;

namespace Library.Application.Queries.Members;
public interface IMemberQueries : IQuery
{
    Task<IEnumerable<GetMemberDTO>> GetAllAsync();
    Task<IEnumerable<GetMemberDTO>> GetActiveAsync();
    Task<GetMemberDTO> GetByIdAsync(Guid id);
}

using Library.Application.ViewModel.Books;
using Library.Infrastructure.Services.Queries;

namespace Library.Application.Queries.Books;
public interface IBookQueries : IQuery
{
    Task<IEnumerable<GetBookDTO>> GetAllAsync();
    Task<IEnumerable<GetBookDTO>> GetActiveAsync();
    Task<GetBookDTO> GetByIdAsync(Guid id);
}

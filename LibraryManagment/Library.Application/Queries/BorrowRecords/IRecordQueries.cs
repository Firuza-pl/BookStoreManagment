using Library.Application.ViewModel.Books;
using Library.Infrastructure.Services.Queries;

namespace Library.Application.Queries.BorrowRecords;
public interface IRecordQueries : IQuery
{
    Task<IEnumerable<GetRecordDTO>> GetAllAsync();
    Task<GetRecordDTO> GetByIdAsync(Guid id);
}

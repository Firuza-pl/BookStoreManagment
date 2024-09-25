using AutoMapper;
using Library.Application.ViewModel.Books;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Queries.BorrowRecords
{
    public class RecordQueries : IRecordQueries
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public RecordQueries(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetRecordDTO>> GetAllAsync()
        {
            var entity = await _context.BorrowRecords.ToListAsync();  //MUST TO DO: getting information along corresponding ID
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            var outputModel = _mapper.Map<IEnumerable<GetRecordDTO>>(entity);
            return outputModel;
        }

        public async Task<GetRecordDTO> GetByIdAsync(Guid id)
        {
            var entity = await _context.BorrowRecords.FirstOrDefaultAsync(x => x.Id == id);
            var outputModel = _mapper.Map<GetRecordDTO>(entity);
            return outputModel;
        }
    }
}

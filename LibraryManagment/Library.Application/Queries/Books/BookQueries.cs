using AutoMapper;
using Library.Application.ViewModel.Books;
using Library.Infrastructure.Persistence;
using Library.SharedKernel.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Queries.Books
{
    public class BookQueries : IBookQueries
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public BookQueries(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetBookDTO>> GetAllAsync()
        {
            var entity = await _context.Books.Where(x => x.IsAvailable == BookStatus.Active || x.IsAvailable == BookStatus.DeActive).ToListAsync();
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            var outputModel = _mapper.Map<IEnumerable<GetBookDTO>>(entity);
            return outputModel;
        }

        public async Task<IEnumerable<GetBookDTO>> GetActiveAsync()
        {
            var entity = await _context.Books.Where(x => x.IsAvailable == BookStatus.Active).ToListAsync();
            var outputModel = _mapper.Map<IEnumerable<GetBookDTO>>(entity);
            return outputModel;
        }

        public async Task<GetBookDTO> GetByIdAsync(Guid id)
        {
            var entity = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            var outputModel = _mapper.Map<GetBookDTO>(entity);
            return outputModel;
        }
    }
}

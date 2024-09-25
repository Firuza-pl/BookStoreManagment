using AutoMapper;
using Library.Application.ViewModel.Books;
using Library.Domain.Entites.BookAggregate;

namespace LibraryManagment.Mapper;
public class BorrowRecordProfile : Profile
{
    public BorrowRecordProfile()
    {
        CreateMap<BorrowRecord, GetRecordDTO>().ReverseMap();
    }
}

using AutoMapper;
using Library.Application.ViewModel.Books;
using Library.Domain.Entites.BookAggregate;

namespace LibraryManagment.Mapper;
public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, GetBookDTO>().ReverseMap();
    }
}

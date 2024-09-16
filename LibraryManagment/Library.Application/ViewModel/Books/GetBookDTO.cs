using Library.SharedKernel.Domain.Enums;

namespace Library.Application.ViewModel.Books;

public class GetBookDTO
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public BookStatus? IsAvailable { get; set; }
}
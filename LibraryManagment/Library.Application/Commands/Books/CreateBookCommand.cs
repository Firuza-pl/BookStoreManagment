using MediatR;
namespace Library.Application.Commands.Books;
public class CreateBookCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string? Title {  get; set; }

    public CreateBookCommand(Guid id, string? title)
    {
        Id = id;
        Title = title;
    }
}

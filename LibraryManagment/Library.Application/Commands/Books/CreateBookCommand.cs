using MediatR;
namespace Library.Application.Commands.Books;
public class CreateBookCommand : IRequest<bool>
{
    public string? Title {  get; set; }

    public CreateBookCommand(string? title)
    {
        Title = title;
    }
}

using MediatR;

namespace Library.Application.Commands.Books;
public class UpdateBookCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string? Title { get; set; }

    public UpdateBookCommand(Guid id, string? title)
    {
        Id= id;
        Title= title;
    }
}

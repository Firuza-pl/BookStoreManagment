using MediatR;

namespace Library.Application.Commands.Books
{
    public class DeleteBookCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public DeleteBookCommand(Guid id)
        {
            Id = id;
        }
    }
}

using Library.Domain.Interface;
using MediatR;
using Library.Domain.Entites.BookAggregate;

namespace Library.Application.Commands.Books
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, bool>
    {
        private readonly IBookRepository _bookRepository;
        public CreateBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

       public async Task<Book> Handle(CreateBookCommand createBookCommand, CancellationToken cancellationToken)
        {
            var book = new Book(createBookCommand.Id, createBookCommand.Title);

            await _bookRepository.AddAsync(book);

            await 

        }
    }
}

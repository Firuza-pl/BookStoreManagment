using Library.Domain.Interface;
using MediatR;
namespace Library.Application.Commands.Books
{
    public class CreateBorrowBookCommandHandler : IRequestHandler<CreateBorrowBookCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBorrowBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreateBorrowBookCommand command, CancellationToken cancellationToken)
        {
            // Get the book from the repository
            var book = await _unitOfWork.BookRepository.GetAsync(command.BookId);

            if (book == null)
            {
                throw new Exception("Book not found");
            }

            // Perform the borrow action on the book
            book.BorrowingBook(command.BookId, command.MemberId, command.BorrowDate);


            //// Manually dispatch the event for testing
            //var domainEvents = book.DomainEvents.ToArray();
            //book.RemoveDoaminEvent();
            //await _unitOfWork.DomainEventDispatcher.DispatchEventsAsync(domainEvents);

            // Save the changes
            await _unitOfWork.SaveAsync(cancellationToken);

            return true;
        }
    }

}

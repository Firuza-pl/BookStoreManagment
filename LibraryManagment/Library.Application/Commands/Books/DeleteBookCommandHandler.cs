using Library.Domain.Interface;
using Library.Infrastructure.Idempotency;
using Library.Infrastructure.Services.Commands;
using MediatR;

namespace Library.Application.Commands.Books
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(DeleteBookCommand deleteBookCommand, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.BookRepository.GetAsync(deleteBookCommand.Id);
           
            if (book is null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            book.Delete();

            var result = await _unitOfWork.SaveAsync(cancellationToken);

            return book.Id;
        }

        public class BookIdentifiedCommandHandler : IdentifiedCommandHandler<DeleteBookCommand, Guid>
        {
            public BookIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
            {
            }
        }

    }
}
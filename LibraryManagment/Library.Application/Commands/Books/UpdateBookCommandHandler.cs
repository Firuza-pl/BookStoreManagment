using Library.Domain.Interface;
using Library.Infrastructure.Idempotency;
using Library.Infrastructure.Services.Commands;
using MediatR;

namespace Library.Application.Commands.Books;
public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBookCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(UpdateBookCommand updateBookCommand, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.BookRepository.GetAsync(updateBookCommand.Id);

        if (book is null)
        {
            throw new ArgumentNullException(nameof(book));
        }

        book.Edit(updateBookCommand.Title);

        await _unitOfWork.SaveAsync(cancellationToken);

        return book.Id;

    }

    public class BookIentifiedCommandHandler : IdentifiedCommandHandler<UpdateBookCommand, Guid>
    {
        public BookIentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }
    }

}

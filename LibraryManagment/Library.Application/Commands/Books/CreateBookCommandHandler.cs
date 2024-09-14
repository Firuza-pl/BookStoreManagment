using Library.Domain.Interface;
using MediatR;
using Library.Domain.Entites.BookAggregate;
using Library.Infrastructure.Idempotency;
using Library.Infrastructure.Services.Commands;

namespace Library.Application.Commands.Books
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(_unitOfWork));
        }

       public async Task<bool> Handle(CreateBookCommand createBookCommand, CancellationToken cancellationToken)
        {
            var book = new Book(createBookCommand.Id, createBookCommand.Title);

            await _unitOfWork.BookRepository.AddAsync(book);

            await _unitOfWork.Save();
            return true;

        }

        public class CityCommandHandler : IdentifiedCommandHandler<CreateBookCommand, bool>
        {
            public CityCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
            {
            }

            protected override bool CreateResultForDuplicateRequest()
            {
                return true; // Ignore duplicate requests
            }
        }

    }
}

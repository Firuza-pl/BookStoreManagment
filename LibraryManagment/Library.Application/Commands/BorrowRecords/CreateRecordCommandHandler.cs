using Library.Domain.Interface;
using MediatR;
using Library.Domain.Entites.BookAggregate;
using Library.Infrastructure.Idempotency;
using Library.Infrastructure.Services.Commands;

namespace Library.Application.Commands.BorrowRecords
{
    public class CreateRecordCommandHandler : IRequestHandler<CreateRecordCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateRecordCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(_unitOfWork));
        }

       public async Task<bool> Handle(CreateRecordCommand createRecordCommand, CancellationToken cancellationToken)
        {
            var record = new BorrowRecord(createRecordCommand.BookId, createRecordCommand.MemberId, createRecordCommand.BorrowDate);

            if(record is null)
                throw new ArgumentNullException(nameof(record));

            await _unitOfWork.BorrowBookRepository.AddAsync(record);

            await _unitOfWork.SaveAsync(cancellationToken);

            return true;

        }

        // implementation for handling duplicate request,It just ensures that no other request exists with the same ID
        public class BorrowIdentifedCommandHandler : IdentifiedCommandHandler<CreateRecordCommand, bool>
        {
            public BorrowIdentifedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
            {
            }

            protected override bool CreateResultForDuplicateRequest()
            {
                return true; // Ignore duplicate requests
            }
        }

    }
}

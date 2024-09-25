using Library.Domain.Interface;
using Library.Infrastructure.Idempotency;
using Library.Infrastructure.Services.Commands;
using MediatR;

namespace Library.Application.Commands.BorrowRecords
{
    public class DeleteRecordCommandHandler : IRequestHandler<DeleteRecordCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteRecordCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(DeleteRecordCommand deleteRecordBookCommand, CancellationToken cancellationToken)
        {
            var record = await _unitOfWork.BorrowBookRepository.GetAsync(deleteRecordBookCommand.Id);
           
            if (record is null)
                throw new ArgumentNullException(nameof(record));

            record.Delete();

            var result = await _unitOfWork.SaveAsync(cancellationToken);

            return record.Id;
        }

        public class BorrowIdentifiedCommandHandler : IdentifiedCommandHandler<DeleteRecordCommand, Guid>
        {
            public BorrowIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
            {
            }
        }

    }
}
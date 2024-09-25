using Library.Domain.Interface;
using Library.Infrastructure.Idempotency;
using Library.Infrastructure.Services.Commands;
using MediatR;

namespace Library.Application.Commands.BorrowRecords;
public class UpdateRecordCommandHandler : IRequestHandler<UpdateRecordCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRecordCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(UpdateRecordCommand updateRecordCommand, CancellationToken cancellationToken)
    {
        var record = await _unitOfWork.BorrowBookRepository.GetAsync(updateRecordCommand.Id);

        if (record is null)
            throw new ArgumentNullException(nameof(record));

        record.Edit(updateRecordCommand.BookId,updateRecordCommand.MemberId, updateRecordCommand.BorrowDate);

        await _unitOfWork.SaveAsync(cancellationToken);

        return record.Id;

    }

    public class BorrowIentifiedCommandHandler : IdentifiedCommandHandler<UpdateRecordCommand, Guid>
    {
        public BorrowIentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }
    }

}

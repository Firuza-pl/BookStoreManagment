using MediatR;

namespace Library.Application.Commands.BorrowRecords
{
    public class DeleteRecordCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public DeleteRecordCommand(Guid id)
        {
            Id = id;
        }
    }
}

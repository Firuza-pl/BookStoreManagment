using MediatR;

namespace Library.Application.Commands.BorrowRecords;
public class UpdateRecordCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
    public DateTime BorrowDate { get; set; }

    public UpdateRecordCommand(Guid id, Guid bookId, Guid memberId, DateTime borrowDate) => (Id, BookId, MemberId, BorrowDate) = (id, BookId, memberId, borrowDate);

}

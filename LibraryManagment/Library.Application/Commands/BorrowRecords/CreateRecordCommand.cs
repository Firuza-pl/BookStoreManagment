using MediatR;
namespace Library.Application.Commands.BorrowRecords;
public class CreateRecordCommand : IRequest<bool>
{
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
    public DateTime BorrowDate { get; set; }

    public CreateRecordCommand(Guid bookId, Guid memberId, DateTime borrowDate) => (BookId, MemberId, BorrowDate) = (bookId, memberId, borrowDate);

}

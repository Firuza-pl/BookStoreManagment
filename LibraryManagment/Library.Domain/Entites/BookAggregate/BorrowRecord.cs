using Library.Domain.Entites.MemberAggregate;
using Library.Shared.Kernel.Core;
using Library.SharedKernel.Domain.Enums;
using System.Xml.Linq;

namespace Library.Domain.Entites.BookAggregate;
public class BorrowRecord : Entity, IAggregateRoot
{
    public Guid BookId { get; private set; }
    public Guid MemberId { get; private set; }
    public DateTime BorrowDate { get; private set; }
    public RecordStatus IsReturned { get; private set; }
    // Navigation properties
    public Book? Book { get; set; }
    public Member? Member { get; set; }

    public BorrowRecord(Guid bookId, Guid memberId, DateTime borrowDate, RecordStatus isReturned = RecordStatus.Borrowed) => (BookId, MemberId, BorrowDate, IsReturned) = (bookId, memberId, borrowDate, isReturned);

    public void Edit(Guid bookId, Guid memberId, DateTime borrowDate)
    {
        BookId = bookId;
        MemberId = memberId;
        BorrowDate = borrowDate;
        IsReturned = RecordStatus.Returned;
    }
    public void Delete()
    {
        IsReturned = RecordStatus.Returned;
    }
}
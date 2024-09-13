using Library.Domain.Entites.MemberAggregate;
using Library.Shared.Kernel.Core;
using Library.SharedKernel.Domain.Enums;

namespace Library.Domain.Entites.BookAggregate;
public class BorrowRecord : Entity, IAggregateRoot
{
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
    public DateTime BorrowDate { get; set; }
    public RecordStatus IsReturned { get; set; }
    // Navigation properties
    public Book? Book { get; set; }
    public Member? Member { get; set; }

    public BorrowRecord(Guid id, Guid bookId, Guid memberId) => (Id, BookId, MemberId) = (id, BookId, memberId);
}
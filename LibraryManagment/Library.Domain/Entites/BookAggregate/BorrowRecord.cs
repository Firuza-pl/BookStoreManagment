using Library.Domain.Core;
using Library.Domain.Entites.MemberAggregate;

namespace Library.Domain.Entites.BookAggregate;
public class BorrowRecord : Entity, IAggregateRoot
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
    public DateTime BorrowDate { get; set; }
    public bool IsReturned { get; set; }
    // Navigation properties
    public Book? Book { get; set; }
    public Member? Member { get; set; }

    public BorrowRecord(Guid id, Guid bookId, Guid memberId) => (Id, BookId, MemberId) = (id, BookId, memberId);
}
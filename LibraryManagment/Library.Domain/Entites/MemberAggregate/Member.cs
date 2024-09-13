using Library.Domain.Entites.BookAggregate;
using Library.Shared.Kernel.Core;

namespace Library.Domain.Entites.MemberAggregate;
public class Member : Entity, IAggregateRoot
{
    public string? Name { get; set; }
    public ICollection<BorrowRecord>? BorrowRecords { get; set; }
    public Member(Guid id, string? name) => (Id, Name) = (id, Name);

    //MUST TO DO: GetBorrowHistory()
}

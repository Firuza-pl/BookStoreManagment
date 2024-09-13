using Library.Domain.Entites.BookAggregate;
using Library.Shared.Kernel.Core;

namespace Library.Domain.Entites.MemberAggregate;
public class Member : Entity, IAggregateRoot
{
    public string? Name { get; private set; }
    public ICollection<BorrowRecord>? BorrowRecords { get;private set; }
    public Member(Guid id, string? name) => (Id, Name) = (id, Name);

    //MUST TO DO: GetBorrowHistory()
}

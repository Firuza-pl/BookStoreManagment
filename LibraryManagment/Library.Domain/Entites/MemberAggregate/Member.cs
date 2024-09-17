using Library.Domain.Entites.BookAggregate;
using Library.Shared.Kernel.Core;
using Library.SharedKernel.Domain.Enums;

namespace Library.Domain.Entites.MemberAggregate;
public class Member : Entity, IAggregateRoot
{
    public string? Name { get; private set; }
    public MemberStatus IsAvailable { get; private set; }
    //other prop can included, this is just testing
    public ICollection<BorrowRecord>? BorrowRecords { get; private set; }
    public Member(string? name, MemberStatus isAvailable = MemberStatus.Active) => (Name, IsAvailable) = (name, isAvailable); //add

    public void Edit(string? name)
    {
        Name = name;
        IsAvailable = MemberStatus.Active;
    }

    public void Delete()
    {
        IsAvailable = MemberStatus.Deactive;
    }
    //MUST TO DO: GetBorrowHistory()
}

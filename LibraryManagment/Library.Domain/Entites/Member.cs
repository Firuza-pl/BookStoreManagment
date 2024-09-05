using Library.Domain.DomainObj;

namespace Library.Domain.Entites;
public class Member: Entity, IAggregateRoot
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    // Navigation property
    public ICollection<BorrowRecord>? BorrowRecords { get; set; }

    public Member(Guid id, string? name)=>(Id, Name)=(id,Name);
}

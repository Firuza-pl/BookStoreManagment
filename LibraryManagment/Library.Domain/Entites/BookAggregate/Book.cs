using Library.Shared.Kernel.Core;
using Library.SharedKernel.Domain.Enums;

namespace Library.Domain.Entites.BookAggregate;
public class Book : Entity, IAggregateRoot
{
    public string? Title { get; private set; }
    public BookStatus IsAvailable { get; private set; }
    // Navigation property
    public ICollection<BorrowRecord>? BorrowRecords { get; set; }
    public Book(Guid id, string? title, BookStatus isAvailable) => (Id, Title, IsAvailable) = (id, title, isAvailable);

    //MUST TO DO: BorrowBook() also raise an event : AddDomainEvent
}

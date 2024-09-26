using Library.Domain.Events;
using Library.Shared.Kernel.Core;
using Library.SharedKernel.Domain.Enums;

namespace Library.Domain.Entites.BookAggregate;
public class Book : Entity, IAggregateRoot
{
    public string? Title { get; private set; }
    public BookStatus IsAvailable { get; private set; }
    // Navigation property
    public ICollection<BorrowRecord>? BorrowRecords { get; set; }  //every Book object will always have a properly initialized BorrowRecords collection from the moment it is created.

    public Book(string? title, BookStatus isAvailable = BookStatus.Active) => (Title, IsAvailable, BorrowRecords) = (title, isAvailable, new List<BorrowRecord>()); //add
    public void Edit(string? title)
    {
        Title = title;
        IsAvailable = BookStatus.Active;

    }

    public void Delete()
    {
        IsAvailable = BookStatus.DeActive;
    }

    public void BorrowingBook(Guid bookId, Guid memberId, DateTime dateTime)
    {
        var record = new BorrowRecord(bookId, memberId, dateTime);
        if (record is null)
            throw new ArgumentNullException(nameof(record));

        BorrowRecords.Add(record);

        AddDomainEvent(new BorrowRecordStatusChangedEvent(record)); //automatically called domain event
    }


    public void ChangeStatus(BookStatus newStatus)
    {
        IsAvailable = newStatus;
    }

}

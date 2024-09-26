using Library.Domain.Entites.BookAggregate;
using Library.Shared.Kernel.Core;
namespace Library.Domain.Events
{
    public class BorrowRecordStatusChangedEvent : DomainEvent
    {
        public BorrowRecord BorrowRecord { get; }

        public BorrowRecordStatusChangedEvent(BorrowRecord borrowRecord)
        {
            BorrowRecord = borrowRecord ?? throw new ArgumentNullException(nameof(borrowRecord));
        }
    }
}

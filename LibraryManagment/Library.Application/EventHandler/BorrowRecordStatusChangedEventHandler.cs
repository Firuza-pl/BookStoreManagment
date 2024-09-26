using Library.Domain.Events;
using Library.Domain.Interface;
using Library.SharedKernel.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.EventHandler
{
    public class BorrowRecordStatusChangedEventHandler : INotificationHandler<BorrowRecordStatusChangedEvent>
    {
        private readonly IUnitOfWork _unitOfWork;

        public BorrowRecordStatusChangedEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(BorrowRecordStatusChangedEvent notification, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.BookRepository.GetAsync(notification.BorrowRecord.BookId);

            if (book is null)
            {
                throw new Exception($"Book with ID {notification.BorrowRecord.BookId} not found");
            }

            book.ChangeStatus(BookStatus.DeActive); //when book borrowed then chanage book status from active to deactive

            try
            {
                await _unitOfWork.SaveAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("The book was updated or deleted by another transaction.", ex);
            }
        }
    }
}

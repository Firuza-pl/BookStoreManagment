using MediatR;

namespace Library.Application.Commands.Books
{
    public class CreateBorrowBookCommand : IRequest<bool>
    {
        public Guid BookId { get; }
        public Guid MemberId { get; }
        public DateTime BorrowDate { get; }

        public CreateBorrowBookCommand(Guid bookId, Guid memberId, DateTime borrowDate)
        {
            BookId = bookId;
            MemberId = memberId;
            BorrowDate = borrowDate;
        }
    }
}

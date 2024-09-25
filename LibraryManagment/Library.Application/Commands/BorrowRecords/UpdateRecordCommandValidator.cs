using FluentValidation;
using Library.Application.Commands.BorrowRecords;

namespace Library.Application.Commands.Books;
    public class UpdateRecordCommandValidator : AbstractValidator<UpdateRecordCommand>
    {
    public UpdateRecordCommandValidator()
    {
        RuleFor(x=>x.BookId).NotEmpty();
        RuleFor(x => x.MemberId).NotEmpty();
        RuleFor(x => x.BorrowDate).NotEmpty();

    }
}

using FluentValidation;
namespace Library.Application.Commands.BorrowRecords;
public class CreateRecordCommandValidator : AbstractValidator<CreateRecordCommand>
{
    public CreateRecordCommandValidator()
    {
        RuleFor(x=>x.BookId).NotEmpty();
        RuleFor(x => x.MemberId).NotEmpty();
        RuleFor(x => x.BorrowDate).NotEmpty();

    }
}

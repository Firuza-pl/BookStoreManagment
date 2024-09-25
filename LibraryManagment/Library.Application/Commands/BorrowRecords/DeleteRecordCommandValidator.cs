using FluentValidation;
namespace Library.Application.Commands.BorrowRecords
{
    public class DeleteRecordCommandValidator: AbstractValidator<DeleteRecordCommand>
    {
        public DeleteRecordCommandValidator()
        {
         RuleFor(x=>x.Id).NotEmpty();   
        }
    }
}

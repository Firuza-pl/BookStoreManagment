using FluentValidation;
namespace Library.Application.Commands.Books
{
    public class DeleteBookCommandValidator: AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
         RuleFor(x=>x.Id).NotEmpty();   
        }
    }
}

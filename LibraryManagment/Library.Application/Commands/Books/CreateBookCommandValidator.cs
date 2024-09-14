using FluentValidation;
namespace Library.Application.Commands.Books;
public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x=>x.Title).NotEmpty();
    }
}

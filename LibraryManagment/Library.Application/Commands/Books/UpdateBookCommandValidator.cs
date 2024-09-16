using FluentValidation;

namespace Library.Application.Commands.Books;
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
    public UpdateBookCommandValidator()
    {
        RuleFor(x=>x.Title).NotEmpty();
    }
}

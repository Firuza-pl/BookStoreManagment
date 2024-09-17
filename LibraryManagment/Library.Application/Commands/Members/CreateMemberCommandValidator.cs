using FluentValidation;

namespace Library.Application.Commands.Members;
public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateMemberCommandValidator()
    {
        RuleFor(x=>x.Name).NotEmpty();
    }
}

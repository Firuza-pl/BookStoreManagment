using FluentValidation;

namespace Library.Application.Commands.Members;
public class DeleteMemberCommandValidator : AbstractValidator<DeleteMemberCommand>
{
    public DeleteMemberCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
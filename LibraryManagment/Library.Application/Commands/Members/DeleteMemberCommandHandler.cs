using Library.Application.Commands.Books;
using Library.Domain.Interface;
using Library.Infrastructure.Idempotency;
using Library.Infrastructure.Services.Commands;
using MediatR;

namespace Library.Application.Commands.Members;
public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteMemberCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(DeleteMemberCommand deleteMemberCommand, CancellationToken cancellationToken)
    {
        var member = await _unitOfWork.MemberRepository.GetAsync(deleteMemberCommand.Id);

        if (member is null)
        {
            throw new ArgumentNullException(nameof(member));
        }

        member.Delete();

        var result = await _unitOfWork.SaveAsync(cancellationToken);

        return member.Id;
    }

    public class MemberdentifiedCommandHandler : IdentifiedCommandHandler<DeleteMemberCommand, Guid>
    {
        public MemberdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }
    }


}

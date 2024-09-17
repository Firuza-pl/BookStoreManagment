using Library.Domain.Interface;
using Library.Infrastructure.Idempotency;
using Library.Infrastructure.Services.Commands;
using MediatR;

namespace Library.Application.Commands.Members;
public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMemberCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(UpdateMemberCommand updateMemberCommand, CancellationToken cancellationToken)
    {
        var member = await _unitOfWork.MemberRepository.GetAsync(updateMemberCommand.Id);

        if (member is null)
            throw new ArgumentNullException(nameof(member));

        member.Edit(updateMemberCommand.Title);

        await _unitOfWork.SaveAsync(cancellationToken);

        return member.Id;

    }

    public class MemberIentifiedCommandHandler : IdentifiedCommandHandler<UpdateMemberCommand, Guid>
    {
        public MemberIentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }
    }

}

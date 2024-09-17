using Library.Domain.Interface;
using Library.Infrastructure.Idempotency;
using Library.Infrastructure.Services.Commands;
using MediatR;
using Library.Domain.Entites.MemberAggregate;

namespace Library.Application.Commands.Members;
public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateMemberCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<bool> Handle(CreateMemberCommand createMemberCommand, CancellationToken cancellationToken)
    {
        Member member = new Member(createMemberCommand.Name);
        if (member is null)
        {
            throw new ArgumentNullException(nameof(member));
        }


        //MUST TO DO: Adding member to user table 
        //MUST TO DO: Add image and use MinIO system

        var result = await _unitOfWork.MemberRepository.AddAsync(member);

        await _unitOfWork.SaveAsync(cancellationToken);

        return true;
    }

    public class MemberIdentifedCommandHandler : IdentifiedCommandHandler<CreateMemberCommand, bool>
    {
        public MemberIdentifedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests
        }
    }

}

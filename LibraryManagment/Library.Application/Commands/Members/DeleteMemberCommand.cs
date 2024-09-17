using MediatR;

namespace Library.Application.Commands.Members;
public class DeleteMemberCommand: IRequest<Guid>
{
    public Guid Id { get; set; }
    public DeleteMemberCommand(Guid id)
    {
        Id=id;
    }
}

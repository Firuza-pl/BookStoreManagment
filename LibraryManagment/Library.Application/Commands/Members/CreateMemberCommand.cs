using MediatR;

namespace Library.Application.Commands.Members;
public class CreateMemberCommand : IRequest<bool>
{
    public string? Name { get; set; }
}

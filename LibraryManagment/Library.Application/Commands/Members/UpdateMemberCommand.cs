using MediatR;

namespace Library.Application.Commands.Members;
public class UpdateMemberCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string? Title { get; set; }

    public UpdateMemberCommand(Guid id, string? title)
    {
        Id = id;
        Title = title;
    }
}

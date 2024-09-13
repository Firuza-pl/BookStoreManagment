using MediatR;

namespace Library.Shared.Kernel.Core;
public interface IDomainEvent : INotification
{
    public DateTime DateOccured { get; }
}

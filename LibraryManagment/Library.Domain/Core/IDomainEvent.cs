using MediatR;

namespace Library.Domain.Core;
public interface IDomainEvent : INotification
{
    public DateTime DateOccured { get; }
}

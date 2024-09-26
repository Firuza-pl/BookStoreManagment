using Library.Shared.Kernel.Core;

namespace Library.Domain.Interface;
public interface IDomainEventDispatcher
{
    Task DispatchEventsAsync(IEnumerable<IDomainEvent> domainEvents);
}

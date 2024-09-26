using Library.Domain.Interface;
using Library.Shared.Kernel.Core;
using MediatR;

namespace Library.Infrastructure.Repositories;
public class DomainEventDispatcher : IDomainEventDispatcher
{
    //IPublisher is responsible for to publish domain events
    private readonly IPublisher _mediator;

    public DomainEventDispatcher(IPublisher mediator)
    {
        _mediator = mediator;
    }

    public async Task DispatchEventsAsync(IEnumerable<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            //iterates through the provided domian events and async publishes each event using mediator object
            await _mediator.Publish(domainEvent);
        }
    }
}

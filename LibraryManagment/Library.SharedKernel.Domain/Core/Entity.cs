namespace Library.Shared.Kernel.Core;
public class Entity
{
    public Guid Id { get; protected set; }

    private readonly List<IDomainEvent> _eventList = new List<IDomainEvent>();
    public Entity()
    {
        Id = Guid.NewGuid();
    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _eventList.Add(domainEvent);  //allow external code to add domain events to list
    }

    public void RemoveDoaminEvent()
    {
        _eventList.Clear();  //clear domain event ensure that they are not processed
    }

    public void GetDomainEvent()
    {
        _eventList.AsEnumerable(); //iterate all domain event without modifying it
    }
}

namespace Library.Shared.Kernel.Core;
public class DomainEvent : IDomainEvent
{
    public DateTime DateOccured { get; }

    public DomainEvent()
    {
        DateOccured= DateTime.Now;
    }
}

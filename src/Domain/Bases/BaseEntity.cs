using Domain.Events;

namespace Domain.Bases;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? EditDate { get; set; }

    private List<IDomainEvent> Events { get; set; } = new();

    public List<IDomainEvent> GetEvents()
    {
        return Events;
    }
    public void AddEvent(IDomainEvent domainEvent)
    {
        Events.Add(domainEvent);
    }
    public void ClearEvents()
    {
        Events.Clear();
    }
    //protected BaseEntity(DateTime createDate, DateTime editDate)
    //{

    //}
}

using Domain.Events;

namespace Domain.Bases;

public abstract class BaseEntity
{
    public decimal Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? EditDate { get; set; }
    public IReadOnlyCollection<IDomainEvent> Events => _Events.AsReadOnly();
    private List<IDomainEvent> _Events = [];

    public void AddEvent(IDomainEvent domainEvent)
    {
        if (_Events is null)
            _Events = new List<IDomainEvent>();

         _Events.Add(domainEvent);
    }
    public void RemoveEvent(IDomainEvent domainEvent)
    {
        if(_Events is not null && _Events.Contains(domainEvent))
            _Events.Remove(domainEvent);
    }
    public void ClearEvents()
    {
        _Events.Clear();
    }
}
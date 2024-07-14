using Domain.Bases;
using Domain.Interfaces.Repository;
using MediatR;

namespace Infra.Data.Repositories;

public class UnitOfWork(AppDbContext dbContext, IMediator mediator) : IUnitOfWork
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<int> SaveChangesAsync()
    {
        await PublishDomainEventsAsync();
        return await _dbContext.SaveChangesAsync();
    }
    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = _dbContext.ChangeTracker.Entries()
                                        .Select(entry => entry.Entity)
                                        .SelectMany(entity =>
                                        {
                                            var baseEntity = entity as BaseEntity;
                                            var domainEvents = baseEntity?.GetEvents();
                                            baseEntity?.ClearEvents();

                                            return domainEvents ?? new();
                                        })
                                        .ToList();

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
    }
}

using Domain.Entities.UserAggregate;
using Infra.DomainEvents;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infra.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<ShortLink> ShortLinks { get; set; }
    private readonly DomainEventDispatcher _eventDispatcher;
    public AppDbContext(DbContextOptions<AppDbContext> options, DomainEventDispatcher eventDispatcher) : base(options)
    {
        _eventDispatcher = eventDispatcher;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

        ChangeTracker.Entries<User>().ToList().ForEach(e =>
        {
            e.Entity.CheckAndExpireShortLinks();
            _eventDispatcher.Dispatch(e.Entity.Events);
            e.Entity.ClearEvents();
        });

        return result;
    }
}

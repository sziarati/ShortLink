using Domain.Entities.ShortLinkAggregate;
using Domain.Entities.UserAggregate;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infra.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<ShortLink> ShortLinks { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
   
}

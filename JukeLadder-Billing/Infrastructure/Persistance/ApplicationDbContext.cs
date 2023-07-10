using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistance;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Franchise> Franchise => Set<Franchise>();
    public DbSet<FeeParameter> FeeParameters => Set<FeeParameter>();
    public DbSet<BillingState> BillingStates => Set<BillingState>();

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
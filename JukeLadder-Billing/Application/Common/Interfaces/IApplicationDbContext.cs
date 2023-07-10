using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<FeeParameter> FeeParameters { get; }
    DbSet<Franchise> Franchise { get; }
    DbSet<BillingState> BillingStates { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

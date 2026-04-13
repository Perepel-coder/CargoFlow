using Microsoft.EntityFrameworkCore;
using CargoFlow.Domain;

namespace CargoFlow.Infrastructure.Persistence;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; set; }
    DbSet<Order> Orders { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

using CargoFlow.Application.Interfaces;
using CargoFlow.Domain;
using CargoFlow.Infrastructure.Persistence;

namespace CargoFlow.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IApplicationDbContext _context;

    public CustomerRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int?> CreateAsync(
        string login,
        string password,
        CancellationToken cancellationToken = default)
    {
        Customer? existing = _context.Customers.SingleOrDefault(c => c.Login == login);
        if (existing != null) return null;

        Customer customer = new() { Login = login, Password = password };

        await _context.Customers.AddAsync(customer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return customer.Id;
    }

    public async Task<int?> CheckAsync(
        string login,
        string password,
        CancellationToken cancellationToken = default)
    {
        return _context.Customers
            .SingleOrDefault(c => c.Login == login && c.Password == password)?.Id;
    }
}

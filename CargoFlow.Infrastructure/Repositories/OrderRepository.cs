using Microsoft.EntityFrameworkCore;
using CargoFlow.Application.Interfaces;
using CargoFlow.Application.Orders.Dtos;
using CargoFlow.Domain;
using CargoFlow.Infrastructure.Persistence;

namespace CargoFlow.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IApplicationDbContext _context;

    public OrderRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(
        int customerId,
        string senderCity,
        string senderAddress,
        string recipientCity,
        string recipientAddress,
        double weight,
        DateTime dateCargo,
        CancellationToken cancellationToken = default)
    {
        var order = new Order
        {
            CustomerId = customerId,
            SenderCity = senderCity,
            SenderAddress = senderAddress,
            RecipientCity = recipientCity,
            RecipientAddress = recipientAddress,
            Weight = weight,
            DateCargo = dateCargo
        };

        await _context.Orders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return order.Id;
    }

    public async Task<List<OrderDto>> GetByCustomerIdAsync(
        int customerId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Where(o => o.CustomerId == customerId)
            .Select(o => new OrderDto(
                o.Id,
                o.CustomerId,
                o.SenderCity,
                o.SenderAddress,
                o.RecipientCity,
                o.RecipientAddress,
                o.Weight,
                o.DateCargo))
            .ToListAsync(cancellationToken);
    }

    public async Task<OrderDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        Order? order = await _context.Orders.SingleOrDefaultAsync(order => order.Id == id);

        if (order == null)
            return null;

        return new OrderDto(order.Id,
                order.CustomerId,
                order.SenderCity,
                order.SenderAddress,
                order.RecipientCity,
                order.RecipientAddress,
                order.Weight,
                order.DateCargo);
    }
}

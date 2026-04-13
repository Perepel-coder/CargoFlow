using CargoFlow.Application.Orders.Dtos;

namespace CargoFlow.Application.Interfaces;

public interface IOrderRepository
{
    Task<int> AddAsync(
        int customerId,
        string senderCity,
        string senderAddress,
        string recipientCity,
        string recipientAddress,
        double weight,
        DateTime dateCargo,
        CancellationToken cancellationToken = default);

    Task<List<OrderDto>> GetByCustomerIdAsync(
        int customerId,
        CancellationToken cancellationToken = default);
}

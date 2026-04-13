using MediatR;
using CargoFlow.Application.Interfaces;

namespace CargoFlow.Application.Orders.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        return await _orderRepository.AddAsync(
            customerId: request.customerId,
            senderCity: request.senderCity,
            senderAddress: request.senderAddress,
            recipientCity: request.recipientCity,
            recipientAddress: request.recipientAddress,
            weight: request.weight,
            dateCargo: request.dateCargo,
            cancellationToken: cancellationToken);
    }
}
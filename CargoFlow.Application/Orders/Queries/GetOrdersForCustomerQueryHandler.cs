using MediatR;
using CargoFlow.Application.Interfaces;
using CargoFlow.Application.Orders.Dtos;

namespace CargoFlow.Application.Orders.Queries;

public class GetOrdersForCustomerQueryHandler : IRequestHandler<GetOrdersForCustomerQuery, List<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersForCustomerQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<OrderDto>> Handle(GetOrdersForCustomerQuery request, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetByCustomerIdAsync(request.customerId, cancellationToken);
    }
}
using MediatR;
using CargoFlow.Application.Orders.Dtos;

namespace CargoFlow.Application.Orders.Queries;

public record GetOrdersForCustomerQuery(int customerId) : IRequest<List<OrderDto>>;
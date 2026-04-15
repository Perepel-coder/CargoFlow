using CargoFlow.Application.Orders.Dtos;
using MediatR;

namespace CargoFlow.Application.Orders.Queries;

public record GetOrderByIdQuery(int id) : IRequest<OrderDto>;

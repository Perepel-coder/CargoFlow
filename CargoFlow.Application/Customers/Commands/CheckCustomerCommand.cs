using MediatR;

namespace CargoFlow.Application.Customers.Commands;

public record CheckCustomerCommand(string login, string password) : IRequest<int?>;

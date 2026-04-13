using MediatR;

namespace CargoFlow.Application.Customers.Commands;

public record CreateCustomerCommand(string login, string password) : IRequest<int?>;

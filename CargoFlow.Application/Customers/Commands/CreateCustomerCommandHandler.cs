using MediatR;
using CargoFlow.Application.Interfaces;

namespace CargoFlow.Application.Customers.Commands;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int?>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<int?> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        return await _customerRepository.CreateAsync(request.login, request.password, cancellationToken);
    }
}

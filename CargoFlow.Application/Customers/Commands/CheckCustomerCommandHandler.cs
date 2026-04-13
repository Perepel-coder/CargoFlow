using MediatR;
using CargoFlow.Application.Interfaces;

namespace CargoFlow.Application.Customers.Commands;

public class CheckCustomerCommandHandler : IRequestHandler<CheckCustomerCommand, int?>
{
    private readonly ICustomerRepository _customerRepository;

    public CheckCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<int?> Handle(CheckCustomerCommand request, CancellationToken cancellationToken)
    {
        return await _customerRepository.CheckAsync(request.login, request.password, cancellationToken);
    }
}

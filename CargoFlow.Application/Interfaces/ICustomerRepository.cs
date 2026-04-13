namespace CargoFlow.Application.Interfaces;

public interface ICustomerRepository
{
    Task<int?> CreateAsync(
        string login,
        string password,
        CancellationToken cancellationToken = default);

    Task<int?> CheckAsync(
        string login,
        string password,
        CancellationToken cancellationToken = default);
}

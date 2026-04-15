using Microsoft.Extensions.DependencyInjection;
using CargoFlow.Application.Interfaces;
using CargoFlow.Infrastructure.Persistence;
using CargoFlow.Infrastructure.Repositories;

namespace CargoFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IApplicationDbContext, ApplicationContext>();

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}

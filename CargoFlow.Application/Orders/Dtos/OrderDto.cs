namespace CargoFlow.Application.Orders.Dtos;

public record OrderDto(
    int Id,
    int CustomerId,
    string SenderCity,
    string SenderAddress,
    string RecipientCity,
    string RecipientAddress,
    double Weight,
    DateTime DateCargo);

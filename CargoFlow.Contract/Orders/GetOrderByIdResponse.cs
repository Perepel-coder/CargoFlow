namespace CargoFlow.Contract.Orders;

public record GetOrderByIdResponse(
    int id,
    int customerId,
    string senderCity,
    string senderAddress,
    string recipientCity,
    string recipientAddress,
    double weight,
    DateTime dateCargo);

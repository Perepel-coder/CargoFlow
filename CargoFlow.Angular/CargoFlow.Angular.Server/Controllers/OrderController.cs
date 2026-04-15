using MediatR;
using Microsoft.AspNetCore.Mvc;
using CargoFlow.Application.Orders.Commands;
using CargoFlow.Application.Orders.Dtos;
using CargoFlow.Application.Orders.Queries;
using CargoFlow.Contract.Orders;

namespace CargoFlow.WebApi.Controllers;

[Route("/order/")]
public class OrderController : Controller
{
    private readonly ISender _sender;

    public OrderController(ISender sender)
    {
        _sender = sender ?? throw new ArgumentNullException(nameof(sender));
    }

    [HttpPost("home/create-order/")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest orderRequest)
    {
        try
        {
            CreateOrderCommand command = new(
                customerId: orderRequest.customerId,
                senderCity: orderRequest.senderCity,
                senderAddress: orderRequest.senderAddress,
                recipientCity: orderRequest.recipientCity,
                recipientAddress: orderRequest.recipientAddress,
                weight: orderRequest.weight,
                dateCargo: orderRequest.dateCargo);

            int id = await _sender.Send(command);

            CreateOrderResponse response = new(
                id: id,
                customerId: orderRequest.customerId,
                senderCity: orderRequest.senderCity,
                senderAddress: orderRequest.senderAddress,
                recipientCity: orderRequest.recipientCity,
                recipientAddress: orderRequest.recipientAddress,
                weight: orderRequest.weight,
                dateCargo: orderRequest.dateCargo);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("home/get-orders-by-customer/")]
    public async Task<IActionResult> GetOrdersForCustomer([FromQuery] GetOrdersForCustomerRequest ordersForCustomerRequest)
    {
        try
        {
            GetOrdersForCustomerQuery query = new(ordersForCustomerRequest.customerId);

            List<OrderDto> orders = await _sender.Send(query);

            if (!orders.Any())
            {
                return NotFound("No orders found for the specified customer");
            }

            GetOrdersForCustomerResponse response = new GetOrdersForCustomerResponse(
                orders: orders.Select(o => new OrdersForCustomer(
                    id: o.Id,
                    customerId: o.CustomerId,
                    senderCity: o.SenderCity,
                    senderAddress: o.SenderAddress,
                    recipientCity: o.RecipientCity,
                    recipientAddress: o.RecipientAddress,
                    weight: o.Weight,
                    dateCargo: o.DateCargo)).ToList());

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("home/get-orders-by-id/")]
    public async Task<IActionResult> GetOrderById([FromQuery] GetOrderByIdRequest getOrderByIdRequest)
    {

        try
        {
            GetOrderByIdQuery query = new(getOrderByIdRequest.id);

            OrderDto order = await _sender.Send(query);

            if (order == null)
            {
                return NotFound("No order found for the id");
            }

            GetOrderByIdResponse response = new(
                id: order.Id,
                customerId: order.CustomerId,
                senderCity: order.SenderCity,
                senderAddress: order.SenderAddress,
                recipientCity: order.RecipientCity,
                recipientAddress: order.RecipientAddress,
                weight: order.Weight,
                dateCargo: order.DateCargo);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

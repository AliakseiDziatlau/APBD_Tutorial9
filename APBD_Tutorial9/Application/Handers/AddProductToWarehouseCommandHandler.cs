using APBD_Tutorial9.Application.Commands;
using APBD_Tutorial9.Domain.Interfaces;
using MediatR;
using Microsoft.Data.SqlClient;

namespace APBD_Tutorial9.Application.Handers;

public class AddProductToWarehouseCommandHandler : IRequestHandler<AddProductToWarehouseCommand, (bool success, string message, int resultId)>
{
    private readonly IProductRepository _productRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly string _connectionString;

    public AddProductToWarehouseCommandHandler(
        IProductRepository productRepository,
        IWarehouseRepository warehouseRepository,
        IOrderRepository orderRepository,
        string connectionString)
    {
        _productRepository = productRepository;
        _warehouseRepository = warehouseRepository;
        _orderRepository = orderRepository;
        _connectionString = connectionString;
    }

    public async Task<(bool success, string message, int resultId)> Handle(AddProductToWarehouseCommand request, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            var product = await _productRepository.GetProductByIdAsync(request.IdProduct);
            if (product is null)
                return (false, "Product not found", -1);

            var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(request.IdWirehouse);
            if (warehouse is null)
                return (false, "Warehouse not found", -1);

            var order = await _orderRepository.GetOrderAsync(request.IdProduct, request.Amount, request.CreatedAt);
            if (order is null)
                return (false, "Order not found", -1);
        
            if ((await _orderRepository.CheckIfOrderExistsInProductWarehouseAsync(order.IdOrder)) is null)
                return (false, "Order not found", -1);
        
            await _orderRepository.UpdateFulfilledAt(order.IdOrder);
        
            var result = await _orderRepository.CreateProductWarehouse(product, warehouse, order);
        
            return (true, "Item was successfully added", result);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return (false, $"Transaction failed: {ex.Message}", -1);
        }
    }
}
namespace APBD_Tutorial9.Domain.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetOrderAsync(int idProduct, int amount, DateTime date);
    Task<int?> CheckIfOrderExistsInProductWarehouseAsync(int orderId);
    Task UpdateFulfilledAt(int orderId);
    Task<int> CreateProductWarehouse(Product product, Warehouse warehouse, Order order);
}
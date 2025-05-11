using APBD_Tutorial9.Domain;
using APBD_Tutorial9.Domain.Interfaces;
using APBD_Tutorial9.Infrastructure.Mappers;
using APBD_Tutorial9.Infrastructure.Repositories.DatabaseUtils;
using APBD_Tutorial9.Infrastructure.SqlCommands;
using APBD_Tutorial9.Infrastructure.SqlExtensions;

namespace APBD_Tutorial9.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase, IOrderRepository
{
    public OrderRepository(string connectionString) : base(connectionString) { }
    
    public async Task<Order?> GetOrderAsync(int idProduct, int amount, DateTime date)
    {
        await using var reader = await FluentSql
            .From(_connectionString)
            .WithSql(OrderSqlCommands.GetOrder)
            .WithParameters(
                ("@IdProduct", idProduct),
                ("@Amount", amount),
                ("@CreatedAt", date))
            .ExecuteReaderAsync();
        
        return await DbUtils.MapSingleAsync(reader, SqlMapper.MapOrder);
    }

    public async Task<int?> CheckIfOrderExistsInProductWarehouseAsync(int orderId)
    {
        await using var reader = await FluentSql
            .From(_connectionString)
            .WithSql(OrderSqlCommands.IsOrderInProductWarehouse)
            .WithParameter("IdOrder", orderId)
            .ExecuteReaderAsync();
        
        if (await reader.ReadAsync())
            return reader.GetInt32(reader.GetOrdinal("IdOrder"));

        return null;
    }

    public async Task UpdateFulfilledAt(int orderId)
    {
        await FluentSql
            .From(_connectionString)
            .WithSql(OrderSqlCommands.SetFulfilledAt)
            .WithParameter("IdOrder", orderId)
            .ExecuteNonQueryAsync();
    }

    public async Task<int> CreateProductWarehouse(Product product, Warehouse warehouse, Order order)
    {
        var result = await FluentSql
            .From(_connectionString)
            .WithSql(OrderSqlCommands.CreateProductWarehouse)
            .WithParameters(
                ("@IdProduct", product.IdProduct),
                ("@IdWarehouse", warehouse.IdWarehouse),
                ("@IdOrder", order.IdOrder),
                ("@Amount", order.Amount),
                ("@Price", order.Amount * product.Price))
            .ExecuteScalarAsync(); 

        return Convert.ToInt32(result);
    }
}
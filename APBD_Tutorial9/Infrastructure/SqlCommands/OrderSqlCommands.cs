namespace APBD_Tutorial9.Infrastructure.SqlCommands;

public static class OrderSqlCommands
{
    public static string GetOrder =
        @"Select * 
        from [Order] 
        where IdProduct = @IdProduct and amount = @Amount and CreatedAt < @CreatedAt";
    
    public static string IsOrderInProductWarehouse = @"SELECT IdOrder FROM Product_Warehouse WHERE IdOrder = @IdOrder";

    public static string SetFulfilledAt = @"UPDATE [Order] SET FulfilledAt = GETDATE() WHERE IdOrder = @IdOrder";

    public static string CreateProductWarehouse =
        @"INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt)
        OUTPUT INSERTED.IdProductWarehouse
        VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, GETDATE())";
}
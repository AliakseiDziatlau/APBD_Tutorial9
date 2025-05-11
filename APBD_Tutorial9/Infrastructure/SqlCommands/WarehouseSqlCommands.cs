namespace APBD_Tutorial9.Infrastructure.SqlCommands;

public static class WarehouseSqlCommands
{
    public static string GetWarehouseById = @"Select * from Warehouse where IdWarehouse = @IdWarehouse";
}
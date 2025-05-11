namespace APBD_Tutorial9.Infrastructure.SqlCommands;

public static class ProductSqlCommands
{
    public static string GetProductById = @"Select * from Product where IdProduct = @IdProduct";
}
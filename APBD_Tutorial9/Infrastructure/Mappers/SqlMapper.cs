using APBD_Tutorial9.Domain;
using Microsoft.Data.SqlClient;

namespace APBD_Tutorial9.Infrastructure.Mappers;

public static class SqlMapper
{
    public static Product MapProduct(SqlDataReader reader)
    {
        return new Product
        {
            IdProduct = reader.GetInt32(reader.GetOrdinal("IdProduct")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Description = reader.GetString(reader.GetOrdinal("Description")),
            Price = (double)reader.GetDecimal(reader.GetOrdinal("Price")),
        };
    }

    public static Warehouse MapWarehouse(SqlDataReader reader)
    {
        return new Warehouse
        {
            IdWarehouse = reader.GetInt32(reader.GetOrdinal("IdWarehouse")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Address = reader.GetString(reader.GetOrdinal("Address")),
        };
    }

    public static Order MapOrder(SqlDataReader reader)
    {
        return new Order
        {
            IdOrder = reader.GetInt32(reader.GetOrdinal("IdOrder")),
            IdProduct = reader.GetInt32(reader.GetOrdinal("IdProduct")),
            Amount = reader.GetInt32(reader.GetOrdinal("Amount")),
            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
            FulfilledAt = reader.GetDateTime(reader.GetOrdinal("FulfilledAt")),
        };
    }
}
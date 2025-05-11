using APBD_Tutorial9.Domain;
using APBD_Tutorial9.Domain.Interfaces;
using APBD_Tutorial9.Infrastructure.Mappers;
using APBD_Tutorial9.Infrastructure.Repositories.DatabaseUtils;
using APBD_Tutorial9.Infrastructure.SqlCommands;
using APBD_Tutorial9.Infrastructure.SqlExtensions;

namespace APBD_Tutorial9.Infrastructure.Repositories;

public class ProductRepository : RepositoryBase, IProductRepository
{
    public ProductRepository(string connectionString) : base(connectionString) { }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        await using var reader = await FluentSql
            .From(_connectionString)
            .WithSql(ProductSqlCommands.GetProductById)
            .WithParameter("@IdProduct", id)
            .ExecuteReaderAsync();
        
        return await DbUtils.MapSingleAsync(reader, SqlMapper.MapProduct);
    }
}
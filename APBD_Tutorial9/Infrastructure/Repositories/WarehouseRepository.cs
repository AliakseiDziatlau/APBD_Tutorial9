using APBD_Tutorial9.Domain;
using APBD_Tutorial9.Domain.Interfaces;
using APBD_Tutorial9.Infrastructure.Mappers;
using APBD_Tutorial9.Infrastructure.Repositories.DatabaseUtils;
using APBD_Tutorial9.Infrastructure.SqlCommands;
using APBD_Tutorial9.Infrastructure.SqlExtensions;

namespace APBD_Tutorial9.Infrastructure.Repositories;

public class WarehouseRepository : RepositoryBase, IWarehouseRepository
{
    public WarehouseRepository(string connectionString) : base(connectionString) { }
    
    public async Task<Warehouse?> GetWarehouseByIdAsync(int id)
    {
        await using var reader = await FluentSql
            .From(_connectionString)
            .WithSql(WarehouseSqlCommands.GetWarehouseById)
            .WithParameter("@IdWarehouse", id)
            .ExecuteReaderAsync();
        
        return await DbUtils.MapSingleAsync(reader, SqlMapper.MapWarehouse);
    }
}
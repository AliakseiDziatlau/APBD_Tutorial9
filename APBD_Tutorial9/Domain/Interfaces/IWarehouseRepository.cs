namespace APBD_Tutorial9.Domain.Interfaces;

public interface IWarehouseRepository
{
    Task<Warehouse?> GetWarehouseByIdAsync(int id);
}
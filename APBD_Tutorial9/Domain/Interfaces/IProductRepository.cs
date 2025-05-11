namespace APBD_Tutorial9.Domain.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(int id);
}
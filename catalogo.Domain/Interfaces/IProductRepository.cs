using catalogo.Domain.Models;

namespace catalogo.Domain.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> GetById(int id);
    Task Create(Product product);
    Task Update(Product product);
    Task Delete(int id);
}
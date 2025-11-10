using catalogo.Domain.Interfaces;
using catalogo.Domain.Models;
using catalogo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace catalogo.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetById(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task Create(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}
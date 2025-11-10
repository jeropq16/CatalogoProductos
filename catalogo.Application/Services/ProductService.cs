using catalogo.Domain.Models;
using catalogo.Domain.Interfaces;

namespace catalogo.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAll()
        
        {
            return await _productRepository.GetAll();
        }

        public async Task<Product?> GetById(int id)
        {
            return await _productRepository.GetById(id);
        }

        public async Task<Product?> Create(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name) || product.Price <= 0)
                return null;

            await _productRepository.Create(product);
            return product;
        }

        public async Task<Product?> Update(Product product)
        {
            var existe = await _productRepository.GetById(product.Id);
            if (existe == null) return null;

            existe.Name = product.Name;
            existe.Description = product.Description;
            existe.Price = product.Price;
            existe.Stock = product.Stock;

            await _productRepository.Update(existe);
            return existe;
        }

        public async Task<Product?> Delete(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null) return null;

            await _productRepository.Delete(id);
            return product;
        }
    }
}
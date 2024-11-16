using Microsoft.EntityFrameworkCore;
using ProductProject.Domain.Entities;
using ProductProject.Domain.Interfaces;
using ProductProject.Infrastructure.Persistence.Context;

namespace ProductProject.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;
        public ProductRepository(ProductContext context)
        {
            _context = context;
        }
        public async Task<Product> DeleteProductAsync(Product model)
        {
            _context.Products.Remove(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> PostProductAsync(Product model)
        {
            await _context.Products.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Product> PutProductAsync(Product model)
        {
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
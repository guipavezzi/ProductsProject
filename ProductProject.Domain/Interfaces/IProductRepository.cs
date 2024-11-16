using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductProject.Domain.Entities;

namespace ProductProject.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(int id);
        Task<Product> PostProductAsync(Product model);
        Task<Product> PutProductAsync(Product model);
        Task<Product> DeleteProductAsync(Product model);
    }
}
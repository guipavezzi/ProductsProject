using ProductProject.Domain.Entities;
using ProductProject.Shared.Requests;
using ProductProject.Shared.Responses;

namespace ProductProject.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsServiceAsync();
        Task<Product> GetProductServiceAsync(int id);
        Task<ResponseJson> PostProductsServiceAsync(RequestProductRegister model);
        Task<ResponseJson> PutProductsServiceAsync(Product model);
        Task<ResponseJson> DeleteProductsServiceAsync(int id);
    }
}
using System.Runtime.CompilerServices;
using ProductProject.Application.Interfaces;
using ProductProject.Domain.Entities;
using ProductProject.Domain.Interfaces;
using ProductProject.Shared.Exceptions;
using ProductProject.Shared.Requests;
using ProductProject.Shared.Responses;

namespace ProductProject.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ResponseJson> DeleteProductsServiceAsync(int id)
        {
            var productToRemove = await _productRepository.GetProductAsync(id);
            var value = await _productRepository.DeleteProductAsync(productToRemove);
            if(value is not null)
                throw new ProductProjectException("Produto não foi removido");
            
            return new ResponseJson("Produto removido com sucesso");
        }

        public Task<Product> GetProductServiceAsync(int id)
        {
            var value = _productRepository.GetProductAsync(id);

            if (value is null)
                throw new NotFoundException("Nenhum Produto Encontrado");

            return value;
        }

        public async Task<List<Product>> GetProductsServiceAsync()
        {
            var values = await _productRepository.GetProductsAsync();
            if (values is null)
                throw new NotFoundException("Nenhum Produto Cadastrado");

            return values;
        }

        public async Task<ResponseJson> PostProductsServiceAsync(RequestProductRegister model)
        {
            Product product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description
            };

            var value = await _productRepository.PostProductAsync(product);

            if (value.Id == 0)
                throw new ProductProjectException("Erro ao Cadastrar o Produto");

            
            return new ResponseJson("Cadastro com Sucesso");
        }


        public async Task<ResponseJson> PutProductsServiceAsync(Product model)
        {
            await _productRepository.PutProductAsync(model);
            if(model.Equals(model))
                throw new ProductProjectException("Produto não alterado");

            return new ResponseJson("Produto Alterado com Sucesso");
            
        }
    }
}
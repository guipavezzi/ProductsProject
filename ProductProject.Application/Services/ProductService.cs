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
            await _productRepository.DeleteProductAsync(productToRemove);
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
            var productToPut = await _productRepository.GetProductAsync(model.Id);

            productToPut.Name = model.Name;
            productToPut.Description = model.Description;
            productToPut.Price = model.Price;

            if(productToPut.Equals(model))
                throw new ProductProjectException("Produto não alterado");

            await _productRepository.PutProductAsync(productToPut);

            return new ResponseJson("Produto Alterado com Sucesso");
            
        }
    }
}
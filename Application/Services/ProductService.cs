using AutoMapper;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Get All Products
        public async Task<List<ProductDto>> GetAllProductsAsync(
    int pageNumber,
    int pageSize)
        {
            var products = await _repository.GetAllAsync();

            products = products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return _mapper.Map<List<ProductDto>>(products);
        }

        // Get Product By Id
        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return null;

            return _mapper.Map<ProductDto>(product);
        }

        // Create Product
        public async Task<ProductDto> CreateProductAsync(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            product.CreatedBy = "Admin";
            product.CreatedOn = DateTime.Now;

            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }

        // Update Product
        public async Task<bool> UpdateProductAsync(int id, UpdateProductDto productDto)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return false;

            _mapper.Map(productDto, product);

            product.ModifiedBy = "Admin";
            product.ModifiedOn = DateTime.Now;

            await _repository.UpdateAsync(product);
            await _repository.SaveChangesAsync();

            return true;
        }

        // Delete Product
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return false;

            await _repository.DeleteAsync(product);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
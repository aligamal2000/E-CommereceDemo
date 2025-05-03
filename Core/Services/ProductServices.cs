using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraction;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Products;
using Services.Speifications;
using Shared;
using Shared.Dto_s;

namespace Services
{
    public class ProductServices(IUnitOfWork unitOfWork, IMapper mapper) : IProductServices
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var repository = unitOfWork.GetRepository<ProductBrand, int>();
            var brands = await repository.GetAllAsync();
            var mappedBrands = mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(brands);
            return mappedBrands;
        }

        public async Task<PaginatedResult<ProductDto>> GetAllProductAsync(ProductQueryParams productQuery)
        {
            var repository = unitOfWork.GetRepository<Product, int>();
            var spec = new ProductWithBrandAndTypeSpecification(productQuery);
            var products = await repository.GetAllAsync(spec);

            var mappedProducts = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            var countedProducts = products.Count();
            var CountSpec = new ProductCountSpecification(productQuery);
            var TotalCount = await repository.CountAsync(CountSpec);

            return new PaginatedResult<ProductDto>(productQuery.PageIndex, countedProducts, TotalCount, mappedProducts);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypeAsync()
        {
            var repository = unitOfWork.GetRepository<ProductType, int>();
            var types = await repository.GetAllAsync();
            var mappedTypes = mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(types);
            return mappedTypes;
        }

        public async Task<ProductDto> GetProductAsync(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecification(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec);
            if(product is null)
                throw new ProductNotFoundException(id);
            return mapper.Map<Product, ProductDto>(product);

        }
    }
}

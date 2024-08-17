using AutoMapper;
using Store.Data.Entites;
using Store.Reposatry.Interfaces;
using Store.Reposatry.Specification.Product;
using Store.Service.Helper;
using Store.Service.Services.ProductServices;
using Store.Service.Services.ProductServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductService( IUnitOfWork unitOfWork, IMapper mapper )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync()
        {
            var brands =await unitOfWork.Repositpry<ProductBrand,int>().GetByAllAsync();
            var mappedBrands = mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(brands);
            return mappedBrands;
        }

        public async Task<PaginatedResultDto<ProductDetailsDto>> GetAllProductsAsync(ProductSpecification input)
        {
            var specs = new ProductsWithSpecifications(input);
        
            var products = await unitOfWork.Repositpry<Product, int>().GetByAllWithSpecificationAsync(specs);
            var countSpecs = new ProductsWithFilterAndCountSpecufications(input);
            var count =await unitOfWork.Repositpry<Product,int>().CountSpecificationAsync(countSpecs);
            var mappedProducts = mapper.Map<IReadOnlyList<ProductDetailsDto>>(products);
            return new PaginatedResultDto<ProductDetailsDto>(input.PageSize,input.PageSize,count, mappedProducts);
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await unitOfWork.Repositpry< ProductType, int>().GetByAllAsync();

            var mqppedTypes = mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(types);
            return mqppedTypes;
        }

        public async Task<ProductDetailsDto> GetProductByIdAsync(int? id)
        {
            if (id is null)
            {
                throw new Exception("id null");
            }
            var specs = new ProductsWithSpecifications(id);
            var product = await unitOfWork.Repositpry<Product, int>().GetWithSpecificationByIdAsync(specs);
            if (product is null)
            {
                throw new Exception("product null");

            }

            var mqppedProduct = mapper.Map<ProductDetailsDto>(product);
            return mqppedProduct;
        }
    }
}

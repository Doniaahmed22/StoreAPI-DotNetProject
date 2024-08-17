using Store.Reposatry.Specification.Product;
using Store.Service.Helper;
using Store.Service.Services.ProductServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.ProductServices
{
    public interface IProductService
    {
        Task<ProductDetailsDto> GetProductByIdAsync(int? id);

        Task<PaginatedResultDto<ProductDetailsDto>> GetAllProductsAsync(ProductSpecification input);
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync();

        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.Dto_s;

namespace Abstraction
{
    public interface IProductServices
    {
        Task<PaginatedResult<ProductDto>> GetAllProductAsync(ProductQueryParams productQuery); // ✅ Corrected here
        Task<ProductDto> GetProductAsync(int id);
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeDto>> GetAllTypeAsync();
    }

}

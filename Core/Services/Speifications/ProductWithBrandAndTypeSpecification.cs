using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Products;
using Shared;

namespace Services.Speifications
{
    public class ProductWithBrandAndTypeSpecification:BaseSpecifications<Product,int>

    {
        public ProductWithBrandAndTypeSpecification(ProductQueryParams productQuery)
            : base(P => (productQuery.BrandId.HasValue || P.BrandId == productQuery.BrandId)
                      && (!productQuery.TypeId.HasValue || P.TypeId == productQuery.TypeId)
           &&(string.IsNullOrEmpty(productQuery.SearchValue)|| P.Name.ToLower().Contains(productQuery.SearchValue)))

        {
            AddInclude(P => P.Brand);
            AddInclude(P => P.Type);

            switch (productQuery.SortingOptions)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrdetBy(P => P.Name);
                    break;
                    case ProductSortingOptions.NameDesc:
                    AddOrdetByDesc(P=> P.Name);
                    break;
                case ProductSortingOptions.PriceASC:
                AddOrdetBy(P => P.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrdetByDesc(P=>P.Price);
                    break;
                default:
                    break;
                 

            }
            ApplyPagination(  productQuery.PageSize, productQuery.PageIndex);
        }
        public ProductWithBrandAndTypeSpecification(int id):base(P=>P.id==id)
        {
            AddInclude(P=>P.Brand);
            AddInclude(P=>P.Type);
        }
    }
}

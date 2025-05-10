using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.Products;
using Shared.Dto_s;

namespace Services.MappingProfiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductReslover>());

            CreateMap<DelvieryMethod, BrandDto>();
            CreateMap<ProductType, TypeDto>();
        }   

    }

}

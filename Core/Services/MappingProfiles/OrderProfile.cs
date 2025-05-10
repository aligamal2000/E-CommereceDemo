using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.Orders;
using Shared.Dto_s.IdentityDto;
using Shared.Dto_s.OrderDto;

namespace Services.MappingProfiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile() 
        {
            CreateMap<AddressDto, OrderAddress>();
            CreateMap<Order, OrderReturnDto>()
                .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemPictureUrlResolver>());
            CreateMap<DeliveryMethod, DelvieryMethodDto>();
          
        }
    }
}

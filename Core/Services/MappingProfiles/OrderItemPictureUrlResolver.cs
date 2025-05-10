using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.Orders;
using Shared.Dto_s.OrderDto;

namespace Services.MappingProfiles
{
    public class OrderItemPictureUrlResolver: IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (source.Product.PictureUrl == null)
            {
                return null;
            }
            return source.Product.PictureUrl;
        }
    }
    
}

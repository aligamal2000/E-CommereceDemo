using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dto_s.IdentityDto;

namespace Shared.Dto_s.OrderDto
{
    public class OrderReturnDto
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }= null!;
        public DateTimeOffset OrderDate { get; set; }
        public AddressDto ShipToAddress { get; set; } = null!;
        public string DeliveryMethod { get; set; } = null!;
        public string OrderStatus { get; set; } = null!;
        public ICollection<OrderItemDto> OrderItems { get; set; } = [];
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }


    }
}

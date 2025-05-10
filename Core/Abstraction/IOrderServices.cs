using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dto_s.OrderDto;

namespace Abstraction
{
    public interface IOrderServices
    {
        Task<OrderReturnDto> CreateOrder(OrderDto orderDto ,string Email);
        Task<IEnumerable<DelvieryMethodDto>> GetDeliveryMethodAsync();
        Task<OrderReturnDto> GetOrderByIdAsync(Guid id);
        Task<IEnumerable<OrderReturnDto>> GetAllOrderAsync(string Email);

    }
}

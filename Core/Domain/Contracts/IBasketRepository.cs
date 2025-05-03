using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Baskets;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string basketId);
        Task<CustomerBasket> CreateOrUpdateBasketAsync(CustomerBasket customerBasket,TimeSpan? TimeToLive =null);
        Task<bool> DeleteBasketAsync(string key);

    }
}

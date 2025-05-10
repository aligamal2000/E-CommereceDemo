using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Orders;

namespace Services.Speifications
{
    public class OrderSepcifications:BaseSpecifications<Order,Guid>
    {
        public OrderSepcifications(string email) : base(o => o.UserEmail == email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);
            AddOrdetByDesc(o => o.OrderDate);
        }
        public OrderSepcifications( Guid Id) : base(o => o.id == Id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);
        }

    }
}

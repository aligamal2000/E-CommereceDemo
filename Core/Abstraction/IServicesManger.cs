using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
    public interface IServicesManger
    {
        public IProductServices productServices { get; }
        public IBasketServices basketServices { get; }
        public IAuthenticationServices authenticationServices { get; }
        public IOrderServices orderServices { get; }
    }
}

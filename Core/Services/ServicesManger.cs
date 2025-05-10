using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraction;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Services
{
    public class ServicesManger(IUnitOfWork unitOfWork,IMapper mapper,IBasketRepository basketRepository,UserManager<ApplicationUser> usermanger,IConfiguration configuration  ) : IServicesManger
    {
        private readonly Lazy<IProductServices> _LazyProductServices = new Lazy<IProductServices>(() => new ProductServices(unitOfWork,mapper));
        public IProductServices productServices => _LazyProductServices.Value;
        private readonly Lazy<IBasketServices> _LazyBasket = new Lazy<IBasketServices>(() => new BasketServices(basketRepository, mapper));
        public IBasketServices basketServices => _LazyBasket.Value;
        private readonly Lazy<IAuthenticationServices> _LazyIAuthentication = new Lazy<IAuthenticationServices>(() => new AuthenticationServices(usermanger, configuration,mapper));


        public IAuthenticationServices authenticationServices =>  _LazyIAuthentication.Value;
        private readonly Lazy<IOrderServices> _LazyOrder = new Lazy<IOrderServices>(() => new OrderServices(mapper, basketRepository, unitOfWork));

        public IOrderServices orderServices => _LazyOrder.Value;
    }
}

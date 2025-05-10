using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstraction;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Orders;
using Domain.Models.Products;
using Services.Speifications;
using Shared.Dto_s.IdentityDto;
using Shared.Dto_s.OrderDto;

namespace Services
{
    public class OrderServices(IMapper mapper, IBasketRepository basketRepository, IUnitOfWork unitOfWork) : IOrderServices
    {
        public async Task<OrderReturnDto> CreateOrder(OrderDto orderDto, string email)
        {
            // Map shipping address
            var orderAddress = mapper.Map<AddressDto, OrderAddress>(orderDto.ShipToAddress);

            // Retrieve basket or throw
            var basket = await basketRepository.GetBasketAsync(orderDto.BasketId)
                ?? throw new BasketNotFoundException(orderDto.BasketId);

            // Build order items list
            var orderItems = new List<OrderItem>();
            var productRepo = unitOfWork.GetRepository<Product, int>();

            foreach (var item in basket.Items)
            {
                var product = await productRepo.GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);

                orderItems.Add(new OrderItem
                {
                    Price = product.Price,
                    Quantity = item.Quantity,
                    Product = new ProductItemOrdered
                    {
                        id = product.id,
                        ProductName = product.Name,
                        PictureUrl = product.PictureUrl
                    }
                });
            }

            // Retrieve delivery method or throw
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(orderDto.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);

            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);

            // Create the order
            var order = new Order
            {
                UserEmail = email,
                Subtotal = subtotal,
                ShipToAddress = orderAddress,
                OrderItems = orderItems,
                DeliveryMethod = deliveryMethod,
                DeliveryMethodId = deliveryMethod.id,
                OrderStatus = OrderStatus.Pending,
                OrderDate = DateTimeOffset.Now
            };

            // Save order to DB
            var orderRepo = unitOfWork.GetRepository<Order, Guid>();
       
            await unitOfWork.SaveChangeAsync();

            // Map to DTO and return
            return mapper.Map<OrderReturnDto>(order);
        }

        public async Task<IEnumerable<OrderReturnDto>> GetAllOrderAsync(string email)
        {
            var spec = new OrderSepcifications(email);
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);
            return mapper.Map<IEnumerable<Order>, IEnumerable<OrderReturnDto>>(orders);
        }


        public async Task<IEnumerable<DelvieryMethodDto>> GetDeliveryMethodAsync()
        {
            var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DelvieryMethodDto>>(deliveryMethods);
        }


        public async Task<OrderReturnDto> GetOrderByIdAsync(Guid id)
        {
            var spec = new OrderSepcifications(id);
            var order = await unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(spec);
            if (order is null)
            throw new OrderNotFoundException(id);
            return mapper.Map<Order, OrderReturnDto>(order);
        }
    }
}

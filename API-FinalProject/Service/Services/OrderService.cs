using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Repository.Repositories.Interface;
using Repository.Repositories.Interfaces;
using Service.DTO.Admin.Order;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly IPromoCodeService _promoCodeService;

        public OrderService(IOrderRepository orderRepository, IBasketRepository basketRepository,
                           IPromoCodeService promoCodeService)
        {
            _orderRepository = orderRepository;
            _basketRepository = basketRepository;
            _promoCodeService = promoCodeService;
        }

        //public async Task CreateOrderFromBasketAsync(OrderCreateDto dto)
        //{
        //    var basket = await _basketRepository.GetByUserIdAsync(dto.AppUserId);

        //    if (basket == null || !basket.BasketProducts.Any())
        //        throw new Exception("Basket is empty");

        //    var order = new Order
        //    {
        //        AppUserId = dto.AppUserId,
        //        CreatedDate = DateTime.UtcNow,
        //        StripeSessionId = dto.StripeSessionId,
        //        TotalPrice = basket.BasketProducts.Sum(x => x.Product.Price * x.Quantity),
        //        OrderItems = basket.BasketProducts.Select(x => new OrderItem
        //        {
        //            ProductId = x.ProductId,
        //            Quantity = x.Quantity,
        //            Price = x.Product.Price,
        //            ColorId = x.ColorId
        //        }).ToList()
        //    };

        //    // 1. Order əlavə olunur
        //    await _orderRepository.AddAsync(order);

        //    // 2. Basket silinir
        //    await _basketRepository.DeleteAsync(basket);

        //    // 3. Hər iki əməliyyatı saxlamaq
        //    await _basketRepository.SaveChangesAsync();
        //}

        public async Task CreateOrderFromBasketAsync(OrderCreateDto dto)
        {
            var basket = await _basketRepository.GetByUserIdAsync(dto.AppUserId);

            if (basket == null || !basket.BasketProducts.Any())
                throw new Exception("Basket is empty");

            decimal totalPrice = basket.BasketProducts.Sum(x => x.Product.Price * x.Quantity);

            if (!string.IsNullOrWhiteSpace(dto.PromoCode))
            {
                var promoResult = await _promoCodeService.CheckAndApplyAsync(dto.PromoCode);

                if (promoResult.IsValid)
                {
                    // Endirimi tətbiq edirik
                    decimal discount = (totalPrice * promoResult.DiscountPercent) / 100;
                    totalPrice -= discount;

                    // İstifadə sayını artırırıq
                    await _promoCodeService.IncrementUsageCountAsync(dto.PromoCode);
                }
                else
                {
                    // Əgər promokod yanlışdırsa və bu sənin üçün önəmlidirsə:
                    // throw new Exception(promoResult.Message);
                }
            }

            var order = new Order
            {
                AppUserId = dto.AppUserId,
                CreatedDate = DateTime.UtcNow,
                StripeSessionId = dto.StripeSessionId,
                TotalPrice = totalPrice,
                OrderItems = basket.BasketProducts.Select(x => new OrderItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    Price = x.Product.Price,
                    ColorId = x.ColorId
                }).ToList()
            };

            await _orderRepository.AddAsync(order);
            await _basketRepository.DeleteAsync(basket);
            await _basketRepository.SaveChangesAsync();
        }


        public async Task<List<OrderDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllWithIncludesAsync();
            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                AppUserEmail = o.AppUser.Email,
                CreatedDate = o.CreatedDate,
                TotalPrice = o.TotalPrice,
                Status = o.Status.ToString(),
                Items = o.OrderItems.Select(i => new OrderItemDto
                {
                    ProductName = i.Product.Name,
                    ColorName = i.Color.Name,
                    Price = i.Price,
                    Quantity = i.Quantity
                }).ToList()
            }).ToList();
        }


        public async Task<List<OrderDto>> GetByUserIdAsync(string userId)
        {
            var orders = await _orderRepository.GetByUserIdAsync(userId);
            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                AppUserEmail = "",
                CreatedDate = o.CreatedDate,
                TotalPrice = o.TotalPrice,
                Status = o.Status.ToString(),
                Items = o.OrderItems.Select(i => new OrderItemDto
                {
                    ProductName = i.Product.Name,
                    ColorName = i.Color.Name,
                    Price = i.Price,
                    Quantity = i.Quantity
                }).ToList()
            }).ToList();
        }

        public async Task ChangeStatusAsync(int orderId, string newStatus)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new Exception("Order not found");

            if (!Enum.TryParse<OrderStatus>(newStatus, true, out var parsedStatus))
                throw new Exception("Invalid status value");

            order.Status = parsedStatus;                                                                                           
            await _orderRepository.SaveChangesAsync();                         
        }


    }

}

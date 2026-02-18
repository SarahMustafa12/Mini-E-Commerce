using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mini_E_Commerce.DTOs.Request;
using Mini_E_Commerce.Models;
using Mini_E_Commerce.Repository.IRepository;
using System.Linq.Expressions;

namespace Mini_E_Commerce.Controllers.OrderController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IRepository<Product> _productRepository;
        private IRepository<Order> _orderRepository;
        private IRepository<OrderItem> _orderItemRepository;
        private IOrderRepository _orderRepo;
        public OrderController(IRepository<Product> productRepository, IRepository<Order> orderRepository, IRepository<OrderItem> orderItemRepository, IOrderRepository orderRepo)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _orderRepo = orderRepo;
        }

        [HttpGet("{orderId}/details")]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            var order = await _orderRepo.GetOrderWithItemsAndProducts(orderId);

            if (order == null)
                return NotFound();

            var totalPrice = order.Items.Sum(i => i.Price * i.Quantity);
            var finalPrice =(double) totalPrice * (1 - order.discounts); 

            return Ok(new
            {
                order.Id,
                order.CustomerName,
                order.Email,
                order.Phone,
                order.discounts,
                TotalPrice = totalPrice,
                FinalPrice = finalPrice,
                Items = order.Items.Select(i => new
                {
                    i.Id,
                    i.Quantity,
                    i.Price,
                    Product = new
                    {
                        i.Product.Id,
                        i.Product.Name,
                        i.Product.Price,
                        i.Product.Quantity
                    }
                })
            });
        }



        [HttpPost]

        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = request.Adapt<Order>();

            foreach (var itemRequest in request.Items)
            {
                var product = await _productRepository.GetOneAsync(e => e.Id == itemRequest.ProductId, tracked: false);
                if (product == null)
                    return NotFound($"this product is not found.");

                if (product.Quantity < itemRequest.Quantity)
                    return BadRequest($"there is only {product.Quantity} product.");

                product.Quantity -= itemRequest.Quantity;
                await _productRepository.UpdateAsync(product);

                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = itemRequest.Quantity,
                    Price = (decimal)product.Price
                };

                order.Items.Add(orderItem);
            }

            await _orderRepository.CreateAsync(order);
            await _orderRepository.CommitAsync();

            var getTheOrderItems = await _orderItemRepository.GetAsync(e => e.OrderId == order.Id);
            int totalQuantity = getTheOrderItems.Sum(e => e.Quantity);
            if (totalQuantity >= 2 && totalQuantity <= 4)
            {
                order.discounts = 0.05;
            }
            else if (totalQuantity >= 5)
            {
                order.discounts = 0.1;
            }
            else
            {
                order.discounts = 1;
            }

            await _orderRepository.UpdateAsync(order);


            return Ok(order);
        }


        [HttpGet("gettotalPrice/{orderId}")]
        public async Task<IActionResult> CalculateTotal(int orderId)
        {
            var orderItems = await _orderItemRepository
                .GetAsync(e => e.OrderId == orderId, includes: [e => e.Order]);

            if (!orderItems.Any())
                return NotFound("Order has no items.");

            double discount = orderItems.First().Order.discounts;

            double total =(double) orderItems.Sum(e => e.Price * e.Quantity);


            total = total - (total * discount );

            return Ok(total);
        }



    }
}

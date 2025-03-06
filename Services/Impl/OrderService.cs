using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.Repositories;

namespace G5_MovieTicketBookingSystem.Services.Impl
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly AppDbContext _context;

        public OrderService(IOrderRepository orderRepository,
                            IOrderItemRepository orderItemRepository,
                            AppDbContext context)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _context = context;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task CreateOrderAsync(Order order)
        {
            await _orderRepository.AddOrderAsync(order);
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            await _orderRepository.UpdateOrderStatusAsync(orderId, status); 
        }

        public async Task<bool> CreateOrderWithItemsAsync(Order order, List<OrderItem> orderItems)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
           
                var userExists = await _context.Users.AnyAsync(u => u.UserId == order.UserId);
                if (!userExists)
                {
                    throw new Exception("❌ User không tồn tại! Vui lòng kiểm tra lại.");
                }

             
                _context.Orders.Add(order);
                await _context.SaveChangesAsync(); 

                Console.WriteLine($"✅ Order đã tạo! Order ID: {order.OrderId}");

                Console.WriteLine("✅ OrderItems đã thêm thành công!");
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); 
                Console.WriteLine($"❌ Lỗi khi tạo Order: {ex.Message}");
                return false;
            }
        }

        public async Task<Order> GetLatestOrderByUserIdAsync(int? userId)
        {
            return await _orderRepository.GetLatestOrderByUserIdAsync(userId);
        }

     
     

    }
}

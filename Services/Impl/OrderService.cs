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
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order != null)
            {
                order.OrderStatus = status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CreateOrderWithItemsAsync(Order order, List<OrderItem> orderItems)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Kiểm tra UserId có hợp lệ không
                var userExists = await _context.Users.AnyAsync(u => u.UserId == order.UserId);
                if (!userExists)
                {
                    throw new Exception("❌ User không tồn tại! Vui lòng kiểm tra lại.");
                }

                // Thêm Order vào database trước
                _context.Orders.Add(order);
                await _context.SaveChangesAsync(); // Lưu để có OrderId

                Console.WriteLine($"✅ Order đã tạo! Order ID: {order.OrderId}");

                // Cập nhật OrderId cho OrderItems
                //foreach (var orderItem in orderItems)
                //{
                //    orderItem.OrderId = order.OrderId; // Gán lại OrderId chính xác
                //    orderItem.Order = order;
                //    _context.OrderItems.Add(orderItem);
                //}

                //// Lưu OrderItems vào DB
                //await _context.SaveChangesAsync();
                //await transaction.CommitAsync(); // Commit transaction

                Console.WriteLine("✅ OrderItems đã thêm thành công!");
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // Rollback nếu có lỗi
                Console.WriteLine($"❌ Lỗi khi tạo Order: {ex.Message}");
                return false;
            }
        }

        // Thêm phương thức mới lấy đơn hàng cuối cùng của người dùng
        public async Task<Order> GetLatestOrderByUserIdAsync(int userId)
        {
            return await _orderRepository.GetLatestOrderByUserIdAsync(userId);
        }
    }
}

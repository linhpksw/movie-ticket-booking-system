using G5_MovieTicketBookingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace G5_MovieTicketBookingSystem.Repositories.Impl
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetLatestOrderByUserIdAsync(int? userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.OrderTimestamp)
                .FirstOrDefaultAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            var savedOrder = await GetOrderByIdAsync(order.OrderId);
            if (savedOrder != null)
            {
                Console.WriteLine($"✅ Order đã được lưu vào DB! Order ID: {order.OrderId}");
            }
            else
            {
                Console.WriteLine("❌ Order chưa được lưu vào DB!");
            }
        }
        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == orderId); 

            if (order != null)
            {
                order.OrderStatus = status;
                await _context.SaveChangesAsync();
                Console.WriteLine($"✅ Order với Order ID: {orderId} đã được cập nhật trạng thái thành công!");
            }
            else
            {
                Console.WriteLine($"❌ Không tìm thấy Order với Order ID: {orderId}!");
            }
        }


    }
}

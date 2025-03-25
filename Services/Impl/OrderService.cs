using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.Models;
using G5_MovieTicketBookingSystem.Repositories;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Showtime> GetShowtimeByOrderIdAsync(int orderId)
        {
            // Truy vấn Showtime dựa trên OrderId từ cơ sở dữ liệu
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(ss => ss.Showtime) 
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null || order.OrderItems == null || !order.OrderItems.Any())
            {
                return null; // Trả về null nếu không có OrderItems
            }

            // Lấy Showtime từ OrderItems đầu tiên
            var showtimeId = order.OrderItems.FirstOrDefault().ShowtimeId;
            var showtime = await _context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Screen)
                .FirstOrDefaultAsync(s => s.ShowtimeId == showtimeId);
            return showtime; // Trả về Showtime liên quan đến OrderId
        }


    }
}

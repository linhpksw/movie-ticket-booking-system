using G5_MovieTicketBookingSystem.Data;

namespace G5_MovieTicketBookingSystem.Repositories.Impl
{
    public class OrderItemRepository :IOrderItemRepository
    {
        private readonly AppDbContext _context;

        public OrderItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OrderItem> AddOrderItemAsync(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }


    }
}

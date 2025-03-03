namespace G5_MovieTicketBookingSystem.Services
{
    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(int orderId);
        Task CreateOrderAsync(Order order);
        Task UpdateOrderStatusAsync(int orderId, string status);
        Task<bool> CreateOrderWithItemsAsync(Order order, List<OrderItem> orderItems);
        Task<Order> GetLatestOrderByUserIdAsync(int userId);
    }
}

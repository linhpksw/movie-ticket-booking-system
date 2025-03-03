namespace G5_MovieTicketBookingSystem.Services
{
    public interface IOrderItemService
    {
        Task<OrderItem> AddOrderItemAsync(OrderItem orderItem);

        Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
    }
}

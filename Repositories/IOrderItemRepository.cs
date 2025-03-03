namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface IOrderItemRepository
    {
        Task<OrderItem> AddOrderItemAsync(OrderItem orderItem);

        Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
    }
}

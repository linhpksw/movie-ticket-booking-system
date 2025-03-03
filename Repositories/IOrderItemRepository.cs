namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface IOrderItemRepository
    {
        Task<OrderItem> AddOrderItemAsync(OrderItem orderItem);
      
    }
}

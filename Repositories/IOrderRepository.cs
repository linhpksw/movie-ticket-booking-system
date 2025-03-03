namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(int orderId);
        Task AddOrderAsync(Order order);
      
    }
}

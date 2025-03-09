namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(int orderId);
        Task AddOrderAsync(Order order);
        Task<Order> GetLatestOrderByUserIdAsync(int? userId);

        // Thêm phương thức cập nhật trạng thái đơn hàng
        Task UpdateOrderStatusAsync(int orderId, string status);
    }
}

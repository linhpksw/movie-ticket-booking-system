using G5_MovieTicketBookingSystem.Repositories;

namespace G5_MovieTicketBookingSystem.Services.Impl
{
    public class OrderItemService :IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemService(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<OrderItem> AddOrderItemAsync(OrderItem orderItem)
        {
            return await _orderItemRepository.AddOrderItemAsync(orderItem);
        }
    }
}

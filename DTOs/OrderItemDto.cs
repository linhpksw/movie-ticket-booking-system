namespace G5_MovieTicketBookingSystem.DTOs
{
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public int ScreenSeatId { get; set; }

        public decimal PriceCharged { get; set; }
    }
}

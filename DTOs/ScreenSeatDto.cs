namespace G5_MovieTicketBookingSystem.DTOs
{
    public class ScreenSeatDto
    {
        public int ScreenSeatId { get; set; }
        public string SeatLabel { get; set; }
        public SeatTypeDto? SeatType { get; set; }
        public ScreenDto? Screen { get; set; }
    }
}

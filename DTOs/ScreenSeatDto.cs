namespace G5_MovieTicketBookingSystem.DTOs
{
    public class ScreenSeatDto
    {
        public int ScreenSeatId { get; set; }
        public string SeatLabel { get; set; } = "Unknown Seat"; 

        public SeatTypeDto? SeatType { get; set; }
        public ScreenDto? Screen { get; set; }

        public int ScreenId { get; set; } = 0;
        public int SeatTypeId { get; set; } = 0;

        public ScreenSeatDto() { }
    }
}

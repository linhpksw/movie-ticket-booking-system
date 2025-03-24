namespace G5_MovieTicketBookingSystem.DTOs
{
    public class SeatStatus
    {
        public bool IsAvailable { get; set; }
        public bool IsSold { get; set; }
        // If locked, store the ID of the user who locked the seat.
        public int? LockedBy { get; set; }
    }
}

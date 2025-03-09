namespace G5_MovieTicketBookingSystem.DTOs
{
    public class ShowtimeDto
    {
        public int ShowtimeId { get; set; }
        public DateTime LockStartTime { get; set; }
        public DateTime LockExpiryTime { get; set; }
        public CinemaDto Cinema { get; set; }
        public ScreenDto? Screen { get; set; }
        public int MovieId { get; set; }
        public int ScreenId { get; set; }
        public DateOnly ShowDate { get; set; }
        public TimeOnly ShowTime { get; set; }
        public required string ExperienceType { get; set; }
    }
}

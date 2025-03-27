namespace G5_MovieTicketBookingSystem.DTOs.MovieBookingPlan
{
    public class MovieShowtimeDto
    {
        public string? Title { get; set; }
        public List<ShowTimeDetailDto>? Showtime { get; set; }
    }
    public class ShowTimeDetailDto
    {
        public int ShowtimeId { get; set; }
        public TimeSpan? Showtimehour { get; set; }
        public bool IsSoldOut { get; set; }
    }
}

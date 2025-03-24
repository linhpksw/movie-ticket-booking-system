namespace G5_MovieTicketBookingSystem.DTOs
{
    public class MovieDto
    {
        public int MovieId { get; set; }
        public required string Title { get; set; }
        public required string Genre { get; set; }
        public required string Language { get; set; }
        public float Rating { get; set; }
        public required string Description { get; set; }
        public ICollection<ShowtimeDto>? Showtimes { get; set; }
        public int Duration { get; set; } // in minutes
        public DateTime ReleaseDate { get; set; }
    }

}

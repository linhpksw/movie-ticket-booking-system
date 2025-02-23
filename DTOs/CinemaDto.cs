namespace G5_MovieTicketBookingSystem.DTOs
{
    public class CinemaDto
    {
        public int? CinemaId { get; set; }
        public string CinemaName { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Address { get; set; } = default!;
    }
}

using G5_MovieTicketBookingSystem.DTOs;

namespace G5_MovieTicketBookingSystem.Services
{
    public interface IMovieService
    {
        Task<MovieDto?> GetMovieWithShowtimeAndCinemaAsync(int id);
    }
}

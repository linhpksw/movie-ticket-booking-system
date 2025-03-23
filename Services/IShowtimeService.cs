using G5_MovieTicketBookingSystem.DTOs;

namespace G5_MovieTicketBookingSystem.Services
{
    public interface IShowtimeService
    {
        Task<ShowtimeDto?> GetByIdAsync(int id);

        Task<CinemaDto?> GetCinemaByShowtimeId(int showtimeId);

        Task<List<ShowtimeDto>> GetShowTimeByMovieAndCinemaWithinDay(int movieId, int cinemaId, DateOnly showDate);
    }
}

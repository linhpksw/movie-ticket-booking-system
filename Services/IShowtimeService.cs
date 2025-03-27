using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Services
{
    public interface IShowtimeService
    {
        Task<ShowtimeDto?> GetByIdAsync(int id);

        Task<CinemaDto?> GetCinemaByShowtimeId(int showtimeId);
        Task<Showtime> GetShowtimeByScreenSeatId(int ScreenSeatId);

        Task<List<ShowtimeDto>> GetShowTimeByMovieAndCinemaWithinDay(int movieId, int cinemaId, DateOnly showDate);
        Task<Showtime> getShowTimeById(int id);
        Task<List<Showtime>> GetUpcomingShowtimes();

        void UpdateShowTime(int id);

        Task<IEnumerable<string>> GetExperienceTypeAsync();

    }
}

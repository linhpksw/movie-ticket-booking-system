using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface IShowtimeRepository
    {
        Task<Showtime?> GetByIdAsync(int id);

        Task<Cinema?> GetCinemaByShowtimeId(int showtimeId);

        Task<List<Showtime>> GetShowTimeByMovieAndCinemaWithinDay(int movieId, int cinemaId, DateOnly showDate);

        Task<List<Showtime>> GetUpcomingShowtimes();
        Task<IEnumerable<string>> GetExperienceTypeAsync();
    }
}

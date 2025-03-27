using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface IShowtimeRepository
    {
        Task<Showtime?> GetByIdAsync(int id);

        Task<Cinema?> GetCinemaByShowtimeId(int showtimeId);
        Task<Showtime> GetShowtimeByScreenSeatId(int screenSeatId);
        Task<List<Showtime>> GetShowTimeByMovieAndCinemaWithinDay(int movieId, int cinemaId, DateOnly showDate);

        Task<List<Showtime>> GetUpcomingShowtimes();

        Task<Showtime> getShowTimeById(int id);
        Task UpdateSoldOut(int id);
        Task<ScreenSeat> GetScreenSeatByShowtimeId(int showTimeId);

        Task<IEnumerable<string>> GetExperienceTypeAsync();
    }
}

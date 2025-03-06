using G5_MovieTicketBookingSystem.DTOs.MovieBookingPlan;

namespace G5_MovieTicketBookingSystem.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieShowtimeDto>> GetMovieShowtimeDtos(MovieShowtimeFilterDto filter);
        MovieShowtimeFilterDto InitMovieShowtimeFilter(string city, string cinemaName, string experienceType, DateTime date);
    }
}

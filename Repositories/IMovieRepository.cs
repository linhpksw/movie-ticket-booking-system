using G5_MovieTicketBookingSystem.DTOs.MovieBookingPlan;
using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie?> GetByIdAsync(int id);

        Task<List<Movie?>> GetNowShowingAsync();

        Task<List<Movie?>> GetComingSoonAsync();

        Task<List<Movie?>> GetHotMoviesAsync();
        Task<IEnumerable<MovieShowtimeDto>> GetMovieShowtimeDtos(MovieShowtimeFilterDto movieShowtimeFilterDto);
    }
}

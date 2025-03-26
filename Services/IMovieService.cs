using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;
using System.Threading.Tasks;

namespace G5_MovieTicketBookingSystem.Services
{
    public interface IMovieService
    {
        Task<MovieDto?> GetByIdAsync(int id);
        Task<MovieDto?> GetMovieWithShowtimeAndCinemaAsync(int id);
        Task<List<Movie?>> GetHotMoviesAsync();
        Task<List<Movie?>> GetComingSoonAsync();
        Task<List<Movie?>> GetNowShowingAsync();
    }
}

using G5_MovieTicketBookingSystem.DTOs;

namespace G5_MovieTicketBookingSystem.Services
{
    public interface IMovieService
    {
        Task<MovieDto> GetMovieByIdAsync(int id);

    }
}

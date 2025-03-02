using G5_MovieTicketBookingSystem.DTOs;

namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<MovieShowtimeDto>> GetMovieShowtimeDtos(MovieShowtimeFilterDto movieShowtimeFilterDto);
    }
}

using G5_MovieTicketBookingSystem.DTOs.MovieBookingPlan;

namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<MovieShowtimeDto>> GetMovieShowtimeDtos(MovieShowtimeFilterDto movieShowtimeFilterDto);
    }
}

using G5_MovieTicketBookingSystem.DTOs.MovieBookingPlan;
using G5_MovieTicketBookingSystem.Repositories;

namespace G5_MovieTicketBookingSystem.Services.Impl
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<MovieShowtimeDto>> GetMovieShowtimeDtos(MovieShowtimeFilterDto filter)
        {
        
            return await _movieRepository.GetMovieShowtimeDtos(filter);
        }

        public MovieShowtimeFilterDto InitMovieShowtimeFilter(string city, string cinemaName, string experienceType, DateTime date)
        {
            return new MovieShowtimeFilterDto
            {
                City = city,
                CinemaName = cinemaName,
                ExperienceType = experienceType,
                ShowDate = date
            };
        }
    }
}

using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.DTOs.MovieBookingPlan;
using G5_MovieTicketBookingSystem.Mappers;
using G5_MovieTicketBookingSystem.Models;
using G5_MovieTicketBookingSystem.Repositories;
using System.Text.Json;

namespace G5_MovieTicketBookingSystem.Services.Impl
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IShowtimeRepository _showtimeRepository;
        private readonly ILogger<MovieService> _logger;

        public MovieService(IMovieRepository movieRepository, IShowtimeRepository showtimeRepository, ILogger<MovieService> logger)
        {
            _movieRepository = movieRepository;
            _showtimeRepository = showtimeRepository;
            _logger = logger;
        }

        public async Task<MovieDto?> GetByIdAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            if (movie == null) return null;

            return MovieMapper.ToDto(movie);
        }

        // Get all movies currently showing (Đang khởi chiếu)
        public async Task<List<Movie>> GetNowShowingAsync()
        {
            return await _movieRepository.GetNowShowingAsync();
        }

        // Get all movies coming soon (Sắp khởi chiếu)
        public async Task<List<Movie>> GetComingSoonAsync()
        {
            return await _movieRepository.GetComingSoonAsync();
        }

        // Get top 5 highest-rated movies (Phim HOT)
        public async Task<List<Movie>> GetHotMoviesAsync()
        {
            return await _movieRepository.GetHotMoviesAsync();
        }

        public async Task<MovieDto?> GetMovieWithShowtimeAndCinemaAsync(int id)
        {
            return null;
        }

        public async Task<IEnumerable<MovieShowtimeDto>> GetMovieShowtimeDtos(MovieShowtimeFilterDto filter)
        {
            return await _movieRepository.GetMovieShowtimeDtos(filter);
        }
    }
}

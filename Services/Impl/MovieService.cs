using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Repositories;

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

        public async Task<MovieDto> GetMovieByIdAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            return movie != null ? await MapToDtoAsync(movie) : null; 
        }

        private async Task<MovieDto> MapToDtoAsync(Movie movie)
        {
            var showtimeDtos = new List<ShowtimeDto>();
            foreach (var showtime in movie.Showtimes)
            {
                var showtimeDetail = await _showtimeRepository.GetByIdAsync(showtime.ShowtimeId);
                if (showtimeDetail != null)
                {
                    showtimeDtos.Add(new ShowtimeDto
                    {
                        ShowtimeId = showtimeDetail.ShowtimeId,
                        MovieId = showtimeDetail.MovieId,
                        ScreenId = showtimeDetail.ScreenId,
                        ShowDate = showtimeDetail.ShowDate,
                        ShowTime = showtimeDetail.ShowTime,
                        ExperienceType = showtimeDetail.ExperienceType
                    });
                }
            }

            return new MovieDto
            {
                Title = movie.Title,
                Genre = movie.Genre,
                Language = movie.Language,
                Rating = movie.Rating,
                Description = movie.Description,
                Showtimes = showtimeDtos 
            };
        }
    }
}

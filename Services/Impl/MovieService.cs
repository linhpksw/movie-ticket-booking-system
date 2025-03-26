using G5_MovieTicketBookingSystem.DTOs;
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
            try
            {
                var movie = await _movieRepository.GetByIdAsync(id);
                if (movie == null)
                {
                    _logger.LogWarning($"⚠️ Movie với ID {id} không tồn tại trong database!");
                    return null;
                }

                var showtimes = movie.Showtimes.Select(st => new ShowtimeDto
                {
                    ShowtimeId = st.ShowtimeId,
                    MovieId = st.MovieId,
                    ShowDate = st.ShowDate,
                    ShowTime = st.ShowTime,
                    ExperienceType = st.ExperienceType,

                    Screen = new ScreenDto
                    {
                        ScreenId = st.Screen.ScreenId,
                        ScreenName = st.Screen.ScreenName ?? "Unknown Screen",
                        Cinema = new CinemaDto
                        {
                            CinemaId = st.Screen.Cinema.CinemaId,
                            CinemaName = st.Screen.Cinema.CinemaName ?? "Unknown Cinema",
                            Address = st.Screen.Cinema.Address ?? "No Address",
                            City = st.Screen.Cinema.City ?? "No City"
                        }
                    },

                    ScreenSeats = st.Screen.ScreenSeats.Select(ss => new ScreenSeatDto
                    {
                        ScreenSeatId = ss.ScreenSeatId,
                        SeatLabel = ss.SeatLabel ?? "Unknown Seat",
                        SeatType = new SeatTypeDto
                        {
                            SeatTypeId = ss.SeatType.SeatTypeId,
                            SeatTypeName = ss.SeatType.SeatTypeName ?? "Unknown Type",
                            BasePrice = ss.SeatType.BasePrice
                        }
                    }).ToList()
                }).ToList();

                return new MovieDto
                {
                    MovieId = movie.MovieId,
                    Title = movie.Title ?? "Unknown Movie",
                    Genre = movie.Genre ?? "Unknown Genre",
                    Language = movie.Language ?? "Unknown Language",
                    Rating = movie.Rating,
                    Description = movie.Description ?? "No description available",
                    Showtimes = showtimes
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Lỗi trong GetMovieWithShowtimeAndCinemaAsync: {ex.Message}");
                return null;
            }
        }
    }
}

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

        public async Task<MovieDto?> GetMovieWithShowtimeAndCinemaAsync(int id)
        {
            try
            {
                var movie = await _movieRepository.GetMovieByIdAsync(id);
                if (movie == null)
                {
                    _logger.LogWarning($"⚠️ Movie với ID {id} không tồn tại trong database!");
                    return null;
                }

                // 🔥 Kiểm tra xem dữ liệu có bị null không
                foreach (var showtime in movie.Showtimes)
                {
                    _logger.LogInformation($"🎬 Showtime: {showtime.ShowTime}");
                    if (showtime.ScreenSeat?.Screen != null)
                    {
                        _logger.LogInformation($"✅ ScreenName: {showtime.ScreenSeat.Screen.ScreenName}");
                    }
                    else
                    {
                        _logger.LogWarning($"❌ ScreenName is NULL for showtime {showtime.ShowtimeId}");
                    }

                    if (showtime.ScreenSeat?.SeatType != null)
                    {
                        _logger.LogInformation($"💰 BasePrice: {showtime.ScreenSeat.SeatType.BasePrice}");
                    }
                    else
                    {
                        _logger.LogWarning($"❌ BasePrice is NULL for seat {showtime.ScreenSeatId}");
                    }
                }

                var showtimes = movie.Showtimes.Select(st => new ShowtimeDto
                {
                    ShowtimeId = st.ShowtimeId,
                    MovieId = st.MovieId,
                    ShowDate = st.ShowDate,
                    ShowTime = st.ShowTime,
                    ExperienceType = st.ExperienceType,
                    ScreenSeatId = st.ScreenSeatId,

                    ScreenSeat = st.ScreenSeat != null ? new ScreenSeatDto
                    {
                        ScreenSeatId = st.ScreenSeat.ScreenSeatId,
                        SeatLabel = st.ScreenSeat.SeatLabel ?? "Unknown Seat",
                        SeatType = st.ScreenSeat.SeatType != null ? new SeatTypeDto
                        {
                            SeatTypeId = st.ScreenSeat.SeatType.SeatTypeId,
                            SeatTypeName = st.ScreenSeat.SeatType.SeatTypeName ?? "Unknown Type",
                            BasePrice = st.ScreenSeat.SeatType.BasePrice
                        } : null,
                        Screen = st.ScreenSeat.Screen != null ? new ScreenDto
                        {
                            ScreenId = st.ScreenSeat.Screen.ScreenId,
                            ScreenName = st.ScreenSeat.Screen.ScreenName ?? "Unknown Screen",
                            CinemaId = st.ScreenSeat.Screen.CinemaId
                        } : null
                    } : null,

                    Cinema = st.ScreenSeat?.Screen?.Cinema != null ? new CinemaDto
                    {
                        CinemaId = st.ScreenSeat.Screen.Cinema.CinemaId,
                        CinemaName = st.ScreenSeat.Screen.Cinema.CinemaName ?? "Unknown Cinema",
                        Address = st.ScreenSeat.Screen.Cinema.Address ?? "No Address",
                        City = st.ScreenSeat.Screen.Cinema.City ?? "No City"
                    } : null
                }).ToList();

                return new MovieDto
                {
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

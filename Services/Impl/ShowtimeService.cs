using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Mappers;
using G5_MovieTicketBookingSystem.Models;
using G5_MovieTicketBookingSystem.Repositories;

namespace G5_MovieTicketBookingSystem.Services.Impl
{
    public class ShowtimeService(IShowtimeRepository showtimeRepository, ILogger<ShowtimeService> logger) : IShowtimeService
    {
        private readonly IShowtimeRepository _showtimeRepository = showtimeRepository;
        private readonly ILogger<ShowtimeService> _logger = logger;

        public async Task<ShowtimeDto?> GetByIdAsync(int id)
        {
            var showtime = await _showtimeRepository.GetByIdAsync(id);

            if (showtime == null) return null;

            return ShowtimeMapper.ToDto(showtime);
        }

        public async Task<CinemaDto?> GetCinemaByShowtimeId(int showtimeId)
        {
            var cinema = await _showtimeRepository.GetCinemaByShowtimeId(showtimeId);

            if (cinema == null) return null;

            return CinemaMapper.ToDto(cinema);
        }

        public async Task<List<ShowtimeDto>> GetShowTimeByMovieAndCinemaWithinDay(int movieId, int cinemaId, DateOnly showDate)
        {
            List<Showtime> showtimes = await _showtimeRepository.GetShowTimeByMovieAndCinemaWithinDay(movieId, cinemaId, showDate);

            if (showtimes == null || !showtimes.Any()) return new List<ShowtimeDto>();


            return showtimes.Select(ShowtimeMapper.ToDto).ToList();
        }
        public async Task<Showtime> GetShowtimeByScreenSeatId(int ScreenSeatId)
        {
            return await _showtimeRepository.GetShowtimeByScreenSeatId(ScreenSeatId);
        }

        public async Task<List<Showtime>> GetUpcomingShowtimes()
        {
            return await _showtimeRepository.GetUpcomingShowtimes();
        }

        public async Task<Showtime> getShowTimeById(int id)
        {
            return await _showtimeRepository.getShowTimeById(id);
        }

        public async void UpdateShowTime(int id)
        {
             await _showtimeRepository.UpdateSoldOut(id);
        }

    }
}

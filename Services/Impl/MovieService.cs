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
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if (movie == null)
            {
                Console.WriteLine("Movie not found!");
                return null;
            }

            Console.WriteLine($"Movie: {movie.Title}, Showtimes: {movie.Showtimes.Count}");

            return new MovieDto
            {
                Title = movie.Title,
                Genre = movie.Genre,
                Language = movie.Language,
                Rating = movie.Rating,
                Description = movie.Description,
                Showtimes = movie.Showtimes.Select(st =>
                {
                    Console.WriteLine($"Showtime: {st.ShowtimeId}, ScreenID: {st.Screen.ScreenId}, Cinema: {st.Screen.Cinema.CinemaName}");
                    return new ShowtimeDto
                    {
                        ShowtimeId = st.ShowtimeId,
                        MovieId = st.MovieId,
                        ShowDate = st.ShowDate,
                        ShowTime = st.ShowTime,
                        ExperienceType = st.ExperienceType,
                        Screen = new ScreenDto
                        {
                            ScreenId = st.Screen.ScreenId,
                            ScreenName = st.Screen.ScreenName,
                            CinemaId = st.Screen.CinemaId,
                            ScreenSeats = st.Screen.ScreenSeats.Select(seat =>
                            {
                                Console.WriteLine($"Seat: {seat.SeatLabel}, SeatType: {seat.SeatType.SeatTypeName}");

                                return new ScreenSeatDto
                                {
                                    ScreenSeatId = seat.ScreenSeatId,
                                    SeatLabel = seat.SeatLabel,
                                    SeatType = seat.SeatType != null
                                        ? new SeatTypeDto
                                        {
                                            SeatTypeId = seat.SeatType.SeatTypeId,
                                            SeatTypeName = seat.SeatType.SeatTypeName,
                                            BasePrice = seat.SeatType.BasePrice
                                        }
                                        : null 
                                };
                            }).ToList()
                        },
                        Cinema = new CinemaDto
                        {
                            CinemaId = st.Screen.Cinema.CinemaId,
                            CinemaName = st.Screen.Cinema.CinemaName,
                            Address = st.Screen.Cinema.Address,
                            City = st.Screen.Cinema.City
                        }
                    };
                }).ToList()
            };
        }
    }
}

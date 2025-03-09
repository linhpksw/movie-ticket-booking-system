using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Mappers
{
    public static class MovieMapper
    {
        public static MovieDto ToDto(Movie Movie)
        {
            return new MovieDto
            {
                MovieId = Movie.MovieId,
                Description = Movie.Description,
                Genre = Movie.Genre,
                Language = Movie.Language,
                Title = Movie.Title
            };
        }
    }
}

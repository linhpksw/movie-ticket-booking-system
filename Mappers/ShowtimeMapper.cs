using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Mappers
{
    public static class ShowtimeMapper
    {
        public static ShowtimeDto ToDto(Showtime showtime)
        {
            return new ShowtimeDto
            {
                ShowtimeId = showtime.ShowtimeId,
                MovieId = showtime.MovieId,
                ShowDate = showtime.ShowDate,
                ShowTime = showtime.ShowTime,
                ExperienceType = showtime.ExperienceType
            };
        }
    }
}

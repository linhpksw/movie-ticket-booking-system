using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Mappers
{
    public static class CinemaMapper
    {
        public static CinemaDto ToDto(Cinema cinema)
        {
            return new CinemaDto
            {
                CinemaId = cinema.CinemaId,
                Address = cinema.Address,
                CinemaName = cinema.CinemaName,
                City = cinema.City
            };
        }
    }
}

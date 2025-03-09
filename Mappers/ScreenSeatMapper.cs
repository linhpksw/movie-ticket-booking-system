using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Mappers
{
    public class ScreenSeatMapper
    {
        public static ScreenSeatDto ToDto(ScreenSeat screenSeat)
        {
            return new ScreenSeatDto
            {
                ScreenSeatId = screenSeat.ScreenSeatId,
                ScreenId = screenSeat.ScreenId,
                SeatLabel = screenSeat.SeatLabel,
                SeatTypeId = screenSeat.SeatTypeId
            };
        }
    }
}

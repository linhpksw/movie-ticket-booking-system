using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G5_MovieTicketBookingSystem.Services
{
    public interface IScreenSeatService
    {
        Task<List<ScreenSeat>> GetAllScreenSeatsAsync();
        Task<ScreenSeat?> GetScreenSeatByIdAsync(int screenSeatId);
        Task<ScreenSeat> AddScreenSeatAsync(ScreenSeat screenSeat);
        Task UpdateScreenSeatAsync(ScreenSeat screenSeat);
        Task DeleteScreenSeatAsync(int screenSeatId);
        Task<List<ScreenSeatDto>> GetScreenSeatsByShowtime(int movieId, int cinemaId, DateOnly showDate, TimeOnly showTime);
    }
}

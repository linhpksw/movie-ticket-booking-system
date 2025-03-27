using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;

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
        Task<Dictionary<int, decimal>> GetSeatTypePriceMapAsync();

        Task<ScreenSeat> GetScreenSeatByUserId(int? userId);
    }
}

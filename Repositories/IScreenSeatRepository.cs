using G5_MovieTicketBookingSystem.Models;
namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface IScreenSeatRepository
    {
        Task<List<ScreenSeat>> GetAllScreenSeatsAsync();
        Task<ScreenSeat?> GetScreenSeatByIdAsync(int screenSeatId);
        Task<ScreenSeat> AddScreenSeatAsync(ScreenSeat screenSeat);
        Task UpdateScreenSeatAsync(ScreenSeat screenSeat);
        Task DeleteScreenSeatAsync(int screenSeatId);
        Task<List<ScreenSeat>> GetScreenSeatsByShowtime(int movieId, int cinemaId, DateOnly showDate, TimeOnly showTime);
    }
}

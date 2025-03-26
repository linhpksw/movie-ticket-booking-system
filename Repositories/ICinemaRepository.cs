using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface ICinemaRepository
    {
        Task<List<Cinema>> GetAllAsync();
        Task<Cinema?> GetByIdAsync(int id);
        Task<List<Cinema>> GetCinemasWithScreensAsync();
        Task<IEnumerable<string>> GetAllCitiesAsync();
        Task<IEnumerable<string>> GetAllCinemaAsync();
    }
}

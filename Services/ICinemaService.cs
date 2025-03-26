using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Services
{
    public interface ICinemaService
    {
        Task<List<Cinema>> GetAllAsync();
        Task<CinemaDto?> GetByIdAsync(int id);
    }
}

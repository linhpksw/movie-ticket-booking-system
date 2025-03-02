using G5_MovieTicketBookingSystem.DTOs;

namespace G5_MovieTicketBookingSystem.Services
{
    public interface ICinemaService
    {
        Task<IEnumerable<CinemaDto>> GetAllAsync();
        Task<CinemaDto?> GetByIdAsync(int id);
        Task<CinemaDto> CreateAsync(CinemaDto dto);
        Task<CinemaDto> UpdateAsync(int id, CinemaDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<string>> GetCitiesAsync();
        Task<IEnumerable<string>> GetCinemasAsync();
    }
}

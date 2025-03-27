using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;
using G5_MovieTicketBookingSystem.Repositories;

namespace G5_MovieTicketBookingSystem.Services.Impl
{
    public class CinemaService(ICinemaRepository cinemaRepository, ILogger<CinemaService> logger) : ICinemaService
    {
        private readonly ICinemaRepository _cinemaRepository = cinemaRepository;
        private readonly ILogger<CinemaService> _logger = logger;

        public async Task<List<Cinema>> GetAllAsync()
        {
            return await _cinemaRepository.GetAllAsync();
        }

        public async Task<CinemaDto?> GetByIdAsync(int id)
        {
            var cinema = await _cinemaRepository.GetByIdAsync(id);
            if (cinema == null) return null;

            return new CinemaDto
            {
                CinemaId = cinema.CinemaId,
                CinemaName = cinema.CinemaName,
                City = cinema.City,
                Address = cinema.Address
            };
        }

        public async Task<IEnumerable<string>> GetCitiesAsync()
        {
            try
            {
                return await _cinemaRepository.GetAllCitiesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cities list");
                throw;
            }
        }

        public async Task<IEnumerable<string>> GetCinemasAsync()
        {
            try
            {
                return await _cinemaRepository.GetAllCinemaAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cinemas list");
                throw;
            }
        }
    }
}

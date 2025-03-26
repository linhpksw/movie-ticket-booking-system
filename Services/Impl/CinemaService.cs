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
    }
}

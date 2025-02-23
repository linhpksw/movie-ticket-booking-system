using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Repositories;

namespace G5_MovieTicketBookingSystem.Services.Impl
{
    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepository _cinemaRepository;
        private readonly ILogger<CinemaService> _logger;

        public CinemaService(ICinemaRepository cinemaRepository, ILogger<CinemaService> logger)
        {
            _cinemaRepository = cinemaRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<CinemaDto>> GetAllAsync()
        {
            var entities = await _cinemaRepository.GetAllAsync();
            return entities.Select(c => new CinemaDto
            {
                CinemaId = c.CinemaId,
                CinemaName = c.CinemaName,
                City = c.City,
                Address = c.Address
            });
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

        public async Task<CinemaDto> CreateAsync(CinemaDto dto)
        {
            _logger.LogInformation("CreateAsync called with: {Name}, {City}, {Address}",
                           dto.CinemaName, dto.City, dto.Address);

            var entity = new Cinema
            {
                CinemaName = dto.CinemaName,
                City = dto.City,
                Address = dto.Address
            };
            var created = await _cinemaRepository.CreateAsync(entity);

            _logger.LogInformation("Created a new Cinema with ID {CinemaId}", created.CinemaId);

            dto.CinemaId = created.CinemaId;
            return dto;
        }

        public async Task<CinemaDto> UpdateAsync(int id, CinemaDto dto)
        {
            var existing = await _cinemaRepository.GetByIdAsync(id);
            if (existing == null)
            {
                throw new Exception($"Cinema with ID {id} does not exist.");
            }

            existing.CinemaName = dto.CinemaName;
            existing.City = dto.City;
            existing.Address = dto.Address;

            var updated = await _cinemaRepository.UpdateAsync(existing);

            _logger.LogInformation("Updated Cinema with ID {CinemaId}", updated.CinemaId);

            return new CinemaDto
            {
                CinemaId = updated.CinemaId,
                CinemaName = updated.CinemaName,
                City = updated.City,
                Address = updated.Address
            };
        }

        public async Task DeleteAsync(int id)
        {
            await _cinemaRepository.DeleteAsync(id);
            _logger.LogInformation("Deleted Cinema with ID {CinemaId}", id);
        }
    }
}

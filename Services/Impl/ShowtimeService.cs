using G5_MovieTicketBookingSystem.Repositories;

namespace G5_MovieTicketBookingSystem.Services.Impl
{
    public class ShowtimeService:IShowtimeService
    {
        private readonly IShowtimeRepository _showtimeRepository;

        public ShowtimeService(IShowtimeRepository showtimeRepository)
        {
            _showtimeRepository = showtimeRepository;
        }

        public Task<IEnumerable<string>> GetExperienceTypeAsync()
        {
            return _showtimeRepository.GetExperienceTypeAsync();
        }
    }
}

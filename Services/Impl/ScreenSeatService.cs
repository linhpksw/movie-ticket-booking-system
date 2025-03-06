using G5_MovieTicketBookingSystem.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G5_MovieTicketBookingSystem.Services.Impl
{
    public class ScreenSeatService : IScreenSeatService
    {
        private readonly IScreenSeatRepository _screenSeatRepository;

        public ScreenSeatService(IScreenSeatRepository screenSeatRepository)
        {
            _screenSeatRepository = screenSeatRepository;
        }

        public async Task<List<ScreenSeat>> GetAllScreenSeatsAsync()
        {
            return await _screenSeatRepository.GetAllScreenSeatsAsync();
        }

        public async Task<ScreenSeat?> GetScreenSeatByIdAsync(int screenSeatId)
        {
            return await _screenSeatRepository.GetScreenSeatByIdAsync(screenSeatId);
        }

        public async Task<ScreenSeat> AddScreenSeatAsync(ScreenSeat screenSeat)
        {
            return await _screenSeatRepository.AddScreenSeatAsync(screenSeat);
        }

        public async Task UpdateScreenSeatAsync(ScreenSeat screenSeat)
        {
            await _screenSeatRepository.UpdateScreenSeatAsync(screenSeat);
        }

        public async Task DeleteScreenSeatAsync(int screenSeatId)
        {
            await _screenSeatRepository.DeleteScreenSeatAsync(screenSeatId);
        }
    }
}

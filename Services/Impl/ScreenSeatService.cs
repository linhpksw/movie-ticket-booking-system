using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Mappers;
using G5_MovieTicketBookingSystem.Models;
using G5_MovieTicketBookingSystem.Repositories;
using G5_MovieTicketBookingSystem.Repositories.Impl;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G5_MovieTicketBookingSystem.Services.Impl
{
    public class ScreenSeatService : IScreenSeatService
    {
        private readonly IScreenSeatRepository _screenSeatRepository;
        private readonly ISeatTypeRepository _seatTypeRepository;

        public ScreenSeatService(IScreenSeatRepository screenSeatRepository, ISeatTypeRepository seatTypeRepository)
        {
            _screenSeatRepository = screenSeatRepository;
            _seatTypeRepository = seatTypeRepository;
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

        public async Task<List<ScreenSeatDto>> GetScreenSeatsByShowtime(int movieId, int cinemaId, DateOnly showDate, TimeOnly showTime)
        {
            List<ScreenSeat> screenSeats = await _screenSeatRepository.GetScreenSeatsByShowtime(movieId, cinemaId, showDate, showTime);

            if (screenSeats == null || !screenSeats.Any()) return new List<ScreenSeatDto>();

            return screenSeats.Select(ScreenSeatMapper.ToDto).ToList();
        }

        public async Task<Dictionary<int, decimal>> GetSeatTypePriceMapAsync()
        {
            var seatTypes = await _seatTypeRepository.GetAllSeatTypesAsync();

            if (seatTypes == null || !seatTypes.Any()) return new Dictionary<int, decimal>();

            return seatTypes.ToDictionary(st => st.SeatTypeId, st => st.BasePrice);
        }
    }
}

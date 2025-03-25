using G5_MovieTicketBookingSystem;
using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;
using G5_MovieTicketBookingSystem.Repositories;
using G5_MovieTicketBookingSystem.Repositories;
using G5_MovieTicketBookingSystem.Services;
using Microsoft.AspNetCore.SignalR;

namespace G5_MovieTicketBookingSystem.Services.Impl
{
    public class SeatLockService : ISeatLockService
    {
        private readonly ISeatLockRepository _seatLockRepository;

        public SeatLockService(ISeatLockRepository seatLockRepository)
        {
            _seatLockRepository = seatLockRepository;
        }

        public Task LockSeatAsync(int showtimeId, int userId, int screenSeatId)
        {
            return _seatLockRepository.LockSeatAsync(showtimeId, userId, screenSeatId);
        }

        public Task UnlockSeatAsync(int showtimeId, int userId, int screenSeatId)
        {
            return _seatLockRepository.UnlockSeatAsync(showtimeId, userId, screenSeatId);
        }

        public async Task<Dictionary<int, SeatStatus>> GetSeatsAvailabilityAsync(int showtimeId, List<int> screenSeatIds)
        {
            // Retrieve locked seats along with the user who locked them.
            // This method should return a dictionary of seatId -> lockedBy (e.g., "john_doe")
            var lockedSeats = await _seatLockRepository.GetLockedSeatsWithOwnersAsync(showtimeId, screenSeatIds);

            var soldSeats = await _seatLockRepository.GetSoldSeatsAsync(showtimeId, screenSeatIds);

            var result = new Dictionary<int, SeatStatus>();

            foreach (var seatId in screenSeatIds)
            {
                var status = new SeatStatus
                {
                    // If the seat is sold, mark it as sold.
                    IsSold = soldSeats.Contains(seatId)
                };

                // If the seat is locked, mark it as not available and record who locked it.
                if (lockedSeats.TryGetValue(seatId, out var lockedBy))
                {
                    status.IsAvailable = false;
                    status.LockedBy = lockedBy;
                }
                else
                {
                    // Otherwise, the seat is available.
                    status.IsAvailable = true;
                }

                result.Add(seatId, status);
            }

            return result;
        }

        public Task<SeatLock?> GetUserLockAsync(int showtimeId, int userId)
        {
            return _seatLockRepository.GetUserLockAsync(showtimeId, userId);
        }

        public Task UnlockAllSeatsByExpiryAsync(int showtimeId, int userId, DateTime expiryTime)
        {
            return _seatLockRepository.UnlockAllSeatsByExpiryAsync(showtimeId, userId, expiryTime);
        }
    }
}
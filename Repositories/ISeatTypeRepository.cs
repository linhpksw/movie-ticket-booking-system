using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface ISeatTypeRepository
    {
        Task<List<SeatType>> GetAllSeatTypesAsync();
    }
}

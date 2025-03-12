using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Services
{
    public interface ITicketService
    {
        Task AddTicketAsync(Ticket ticket);
    }
}

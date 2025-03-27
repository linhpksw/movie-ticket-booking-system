using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;
using static G5_MovieTicketBookingSystem.Components.Pages.CheckTicket;

namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface ITicketRepository
    {
        Task AddTicketAsync(Ticket ticket);
        Task<bool> CheckIfTicketExistsAsync(string uniqueCode);

        Task<TicketInfoDto> GetTicketInfoAsync(string uniqueCode);
    }
}

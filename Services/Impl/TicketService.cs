using G5_MovieTicketBookingSystem.Repositories;
using System.Threading.Tasks;

namespace G5_MovieTicketBookingSystem.Services.Impl
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task AddTicketAsync(Ticket ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket), "Ticket cannot be null.");
            }

            await _ticketRepository.AddTicketAsync(ticket);
        }
    }
}

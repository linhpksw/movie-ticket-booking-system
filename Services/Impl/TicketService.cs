using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.Models;
using G5_MovieTicketBookingSystem.Repositories;
using Microsoft.EntityFrameworkCore;

namespace G5_MovieTicketBookingSystem.Services.Impl
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly AppDbContext _context;

        public TicketService(ITicketRepository ticketRepository, AppDbContext context)
        {
            _ticketRepository = ticketRepository;
            _context = context;
        }

        public async Task AddTicketAsync(Ticket ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket), "Ticket cannot be null.");
            }

            await _ticketRepository.AddTicketAsync(ticket);
        }

        public async Task<List<Ticket>> GetTicketsByUserIdAsync(int userId)
        {
            return await _context.Tickets
                .Where(t => t.OrderItem.Order.UserId == userId)
                .ToListAsync();
        }
    }
}

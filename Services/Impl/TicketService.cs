using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.Models;
using G5_MovieTicketBookingSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using G5_MovieTicketBookingSystem.Components.Pages;
using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;
using G5_MovieTicketBookingSystem.Repositories;
using static G5_MovieTicketBookingSystem.Components.Pages.CheckTicket;

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

        public async Task<bool> CheckIfTicketExistsAsync(string uniqueCode)
        {
            if (string.IsNullOrEmpty(uniqueCode))
            {
                throw new ArgumentException("Unique code cannot be null or empty.", nameof(uniqueCode));
            }

            return await _ticketRepository.CheckIfTicketExistsAsync(uniqueCode);
        }

        public async Task<TicketInfoDto> GetTicketInfoAsync(string ticketCode)
        {
           return await _ticketRepository.GetTicketInfoAsync(ticketCode);
        }
    }
}

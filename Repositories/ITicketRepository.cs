namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface ITicketRepository
    {
        Task AddTicketAsync(Ticket ticket);
    }
}

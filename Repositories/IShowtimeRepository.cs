namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface IShowtimeRepository
    {
        Task<Showtime?> GetByIdAsync(int id);
      
    }
}

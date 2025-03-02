namespace G5_MovieTicketBookingSystem.Services
{
    public interface IShowtimeService
    {
        Task<IEnumerable<string>> GetExperienceTypeAsync();
    }
}

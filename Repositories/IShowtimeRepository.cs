namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface IShowtimeRepository
    {
        Task<IEnumerable<string>> GetExperienceTypeAsync();

    }
}

namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface ICinemaRepository
    {
        Task<IEnumerable<Cinema>> GetAllAsync();
        Task<Cinema?> GetByIdAsync(int id);
        Task<Cinema> CreateAsync(Cinema entity);
        Task<Cinema> UpdateAsync(Cinema entity);
        Task DeleteAsync(int id);
    }
}

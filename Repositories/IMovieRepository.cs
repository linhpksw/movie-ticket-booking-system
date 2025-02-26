namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie?> GetByIdAsync(int id);
        Task<Movie> CreateAsync(Movie entity);
        Task<Movie> UpdateAsync(Movie entity);
        Task DeleteAsync(int id);
    }
}

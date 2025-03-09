namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie?> GetMovieByIdAsync(int movieId);
        Task<Movie?> GetByIdAsync(int id);
    }
}

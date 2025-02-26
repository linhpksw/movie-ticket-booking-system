
using G5_MovieTicketBookingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace G5_MovieTicketBookingSystem.Repositories.Impl
{

    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _dbContext;

        public MovieRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Movie> CreateAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _dbContext.Movies
                .Include(m => m.Showtimes)  
                .FirstOrDefaultAsync(m => m.MovieId == id);
        }

        public Task<Movie> UpdateAsync(Movie entity)
        {
            throw new NotImplementedException();
        }
    }
}

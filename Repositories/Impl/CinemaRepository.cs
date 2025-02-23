
using G5_MovieTicketBookingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace G5_MovieTicketBookingSystem.Repositories.Impl
{
    public class CinemaRepository : ICinemaRepository
    {
        private readonly AppDbContext _dbContext;

        public CinemaRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Cinema>> GetAllAsync()
        {
            // Optionally Include(c => c.Screens)
            return await _dbContext.Cinemas.ToListAsync();
        }

        public async Task<Cinema?> GetByIdAsync(int id)
        {
            return await _dbContext.Cinemas
                .FirstOrDefaultAsync(c => c.CinemaId == id);
        }

        public async Task<Cinema> CreateAsync(Cinema entity)
        {
            _dbContext.Cinemas.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Cinema> UpdateAsync(Cinema entity)
        {
            _dbContext.Cinemas.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var cinema = await GetByIdAsync(id);
            if (cinema != null)
            {
                _dbContext.Cinemas.Remove(cinema);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}

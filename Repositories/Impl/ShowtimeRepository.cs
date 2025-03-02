using G5_MovieTicketBookingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace G5_MovieTicketBookingSystem.Repositories.Impl
{
    public class ShowtimeRepository:IShowtimeRepository
{
    private readonly AppDbContext _dbContext;

    public ShowtimeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

        public async Task<IEnumerable<string>> GetExperienceTypeAsync()
        {
          return await _dbContext.Showtimes
                .Select(s => s.ExperienceType)
                .Distinct()
                .OrderBy(s => s)
                .ToListAsync();
        }
    }
}

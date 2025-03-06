using G5_MovieTicketBookingSystem.Repositories;
using G5_MovieTicketBookingSystem;
using G5_MovieTicketBookingSystem.Data;
using Microsoft.EntityFrameworkCore;

public class CinemaRepository : ICinemaRepository
{
    private readonly AppDbContext _context;

    public CinemaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cinema>> GetAllAsync()
    {
        return await _context.Cinemas.Include(c => c.Screens).ToListAsync();
    }

    public async Task<Cinema?> GetByIdAsync(int id)
    {
        return await _context.Cinemas.Include(c => c.Screens).FirstOrDefaultAsync(c => c.CinemaId == id);
    }

    public async Task<Cinema> CreateAsync(Cinema entity)
    {
        _context.Cinemas.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Cinema> UpdateAsync(Cinema entity)
    {
        _context.Cinemas.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var cinema = await GetByIdAsync(id);
        if (cinema != null)
        {
            _context.Cinemas.Remove(cinema);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Cinema>> GetCinemasWithScreensAsync()
    {
        return await _context.Cinemas.Include(c => c.Screens).ToListAsync();
    }
}
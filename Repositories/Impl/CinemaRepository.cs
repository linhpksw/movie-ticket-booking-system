using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.Models;
using G5_MovieTicketBookingSystem.Repositories;
using Microsoft.EntityFrameworkCore;

public class CinemaRepository : ICinemaRepository
{
    private readonly AppDbContext _context;

    public CinemaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Cinema>> GetAllAsync()
    {
        return await _context.Cinemas.ToListAsync();
    }

    public async Task<IEnumerable<string>> GetAllCitiesAsync()
    {
        return await _context.Cinemas
                .Select(c => c.City)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
    }

    public async Task<IEnumerable<string>> GetAllCinemaAsync()
    {
        return await _context.Cinemas
                .Select(c => c.CinemaName)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
    }

    public async Task<Cinema?> GetByIdAsync(int id)
    {
        return await _context.Cinemas.Include(c => c.Screens).FirstOrDefaultAsync(c => c.CinemaId == id);
    }

    public async Task<List<Cinema>> GetCinemasWithScreensAsync()
    {
        return await _context.Cinemas.Include(c => c.Screens).ToListAsync();
    }
}
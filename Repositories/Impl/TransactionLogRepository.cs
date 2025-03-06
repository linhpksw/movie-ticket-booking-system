using G5_MovieTicketBookingSystem.Data;

namespace G5_MovieTicketBookingSystem.Repositories.Impl
{
    public class TransactionLogRepository : ITransactionLogRepository
    {
        private readonly AppDbContext _context;

        public TransactionLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddTransactionLogAsync(TransactionLog transactionLog)
        {
            await _context.TransactionLogs.AddAsync(transactionLog);
            await _context.SaveChangesAsync();
        }
    }
}

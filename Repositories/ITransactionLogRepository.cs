using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface ITransactionLogRepository
    {
        Task AddTransactionLogAsync(TransactionLog transactionLog);
    }
}

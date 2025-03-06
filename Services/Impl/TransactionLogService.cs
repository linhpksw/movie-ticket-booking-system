using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.Repositories;
using System.Threading.Tasks;

namespace G5_MovieTicketBookingSystem.Services.Impl
{
    public class TransactionLogService : ITransactionLogService
    {
        private readonly ITransactionLogRepository _transactionLogRepository;

        public TransactionLogService(ITransactionLogRepository transactionLogRepository)
        {
            _transactionLogRepository = transactionLogRepository;
        }

        public async Task AddTransactionLogAsync(TransactionLog transactionLog)
        {
            if (transactionLog == null)
            {
                throw new ArgumentNullException(nameof(transactionLog), "Transaction log cannot be null.");
            }

            await _transactionLogRepository.AddTransactionLogAsync(transactionLog);
        }
    }
}

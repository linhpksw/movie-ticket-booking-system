namespace G5_MovieTicketBookingSystem.Services
{
    public interface ITransactionLogService
    {
        Task AddTransactionLogAsync(TransactionLog transactionLog);
    }
}

using G5_MovieTicketBookingSystem.Models.VNPAY;

namespace G5_MovieTicketBookingSystem.Services
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);

    }
}

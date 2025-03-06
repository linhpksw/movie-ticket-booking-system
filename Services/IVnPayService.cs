using G5_MovieTicketBookingSystem.DTOs.VNPAY;

namespace G5_MovieTicketBookingSystem.Services
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);

    }
}

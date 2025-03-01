using G5_MovieTicketBookingSystem.libraries;
using G5_MovieTicketBookingSystem.Models.VNPAY;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;

namespace G5_MovieTicketBookingSystem.Services.Impl
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _configuration;

        public VnPayService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreatePaymentUrl(PaymentInformationModel model, HttpContext context)
        {
            try
            {
                var baseUrl = _configuration["Vnpay:BaseUrl"];
                var returnUrl = _configuration["Vnpay:PaymentBackReturnUrl"];

                if (string.IsNullOrEmpty(baseUrl) || string.IsNullOrEmpty(returnUrl))
                {
                    throw new Exception("VNPAY configuration is missing. Please check appsettings.json.");
                }

                var timeNow = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                var tick = DateTime.Now.Ticks.ToString();
                var pay = new VnPayLibrary();

                pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
                pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
                pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
                pay.AddRequestData("vnp_Amount", ((int)model.Amount * 100).ToString());
                pay.AddRequestData("vnp_CreateDate", timeNow);
                pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
                pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
                pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
                pay.AddRequestData("vnp_OrderInfo", $"{model.Name} - {model.OrderDescription} - {model.Amount} VND");
                pay.AddRequestData("vnp_OrderType", model.OrderType);
                pay.AddRequestData("vnp_ReturnUrl", returnUrl);
                pay.AddRequestData("vnp_TxnRef", tick);

                var paymentUrl = pay.CreateRequestUrl(baseUrl, _configuration["Vnpay:HashSecret"]);

                if (string.IsNullOrEmpty(paymentUrl))
                {
                    throw new Exception("Failed to generate VNPAY payment URL.");
                }

                Console.WriteLine($"✅ VNPAY Payment URL Generated: {paymentUrl}");
                return paymentUrl;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi khi tạo URL thanh toán VNPAY: {ex.Message}");
                return null;
            }
        }


        public PaymentResponseModel PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnPayLibrary();
            var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);

            var paymentResponse = new PaymentResponseModel
            {
                OrderDescription = collections["vnp_OrderInfo"],
                TransactionId = collections["vnp_TransactionNo"],
                OrderId = collections["vnp_TxnRef"],
                PaymentMethod = collections["vnp_CardType"],
                PaymentId = collections["vnp_BankTranNo"],
                VnPayResponseCode = collections["vnp_ResponseCode"],
                Success = collections["vnp_ResponseCode"] == "00",
            };

            // Kiểm tra kết quả giao dịch
            if (paymentResponse.Success)
            {
                paymentResponse.PaymentMessage = "Giao dịch thành công!";
            }
            else
            {
                paymentResponse.PaymentMessage = "Giao dịch thất bại! Mã lỗi: " + paymentResponse.VnPayResponseCode;
            }

            return paymentResponse;
        }


    }
}

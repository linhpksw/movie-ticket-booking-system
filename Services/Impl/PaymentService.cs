using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace G5_MovieTicketBookingSystem.Services
{
    public class PaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        public void AddPaymentDetails(int orderId, string paymentGateway, decimal amount, string paymentStatus, string gatewayResponse)
        {
            try
            {
                // Lấy Order từ DB, bao gồm các OrderItem nếu cần
                var order = _context.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.OrderId == orderId);

                if (order != null)
                {
                    // Tạo TransactionLog mới
                    var transactionLog = new TransactionLog
                    {
                        OrderId = orderId,
                        PaymentGateway = paymentGateway,
                        TransactionTimestamp = DateTime.Now,
                        Amount = amount,
                        PaymentStatus = paymentStatus,
                        GatewayResponse = gatewayResponse,
                        Order = order // Thiết lập đối tượng Order liên kết
                    };

                    // Cập nhật trạng thái đơn hàng nếu thanh toán thành công
                    order.OrderStatus = paymentStatus == "Success" ? "Paid" : "Failed";

                    // Thêm TransactionLog vào DB
                    _context.TransactionLogs.Add(transactionLog);

                    // Nếu cần, có thể cập nhật các chi tiết order items tại đây (nếu thanh toán có thay đổi)
                    // Ví dụ: Cập nhật trạng thái hoặc số lượng sản phẩm trong OrderItem.

                    // Lưu tất cả thay đổi vào DB
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Order not found.");
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi tại đây (ví dụ: log lỗi)
                throw new Exception("An error occurred while processing the payment.", ex);
            }
        }
    }
}

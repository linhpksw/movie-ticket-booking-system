using Microsoft.AspNetCore.SignalR;

namespace G5_MovieTicketBookingSystem.Hubs
{
    public class CountdownHub : Hub
    {
        // Gửi thời gian còn lại tới tất cả các client
        public async Task SendTimeToClients(string remainingTime)
        {
            await Clients.All.SendAsync("ReceiveTime", remainingTime);
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace G5_MovieTicketBookingSystem.DTOs.UserDto
{

    public class UserResponseDto
    {
      
        public int UserId { get; set; }

       
        public required string Username { get; set; }

    
        public required string Email { get; set; }
        public required string Fullname { get; set; }

        public ICollection<UserRole>? UserRoles { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<SeatLock>? SeatLocks { get; set; }
        public ICollection<TicketScanLog>? TicketScanLogs { get; set; }
    }
}

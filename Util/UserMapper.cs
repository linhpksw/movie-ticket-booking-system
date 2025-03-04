using G5_MovieTicketBookingSystem.DTOs.UserDto;

namespace G5_MovieTicketBookingSystem.Util
{    //public ICollection<UserRole>? UserRoles { get; set; }
    //public ICollection<Order>? Orders { get; set; }
    //public ICollection<SeatLock>? SeatLocks { get; set; }
    //public ICollection<TicketScanLog>? TicketScanLogs { get; set; }
    public class UserMapper
    {
        public static UserResponseDto MapToUserResponseDto(User user)
        {
            UserResponseDto userResponse = new() {
                UserId=user.UserId,
            Email=user.Email,
            Username = user.Username,
            Fullname=user.Fullname,
            UserRoles = user.UserRoles,
            Orders= user.Orders,
            SeatLocks = user.SeatLocks,
            TicketScanLogs= user.TicketScanLogs

                        };
            return userResponse;
        }

        public static User RequestToUser(UserRequestDto userRequestDto) { return null; }
    }
}

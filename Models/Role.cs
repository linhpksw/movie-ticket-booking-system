using System.ComponentModel.DataAnnotations;

namespace G5_MovieTicketBookingSystem;

public class Role
{
    [Key]
    public int RoleId { get; set; }

    [Required]
    [MaxLength(50)]
    public required string RoleName { get; set; }

    // Navigation
    public ICollection<UserRole>? UserRoles { get; set; }
}

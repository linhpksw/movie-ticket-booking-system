using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G5_MovieTicketBookingSystem;

/// <summary>
/// Many-to-many join table for Users & Roles.
/// </summary>
public class UserRole
{

    
    [Key]
    public int UserId { get; set; }

    [Key]
    public int RoleId { get; set; }


    // Navigation
    [ForeignKey(nameof(UserId))]
    public required User User { get; set; }

    [ForeignKey(nameof(RoleId))]
    public required Role Role { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G5_MovieTicketBookingSystem.Models;

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
    public User User { get; set; }

    [ForeignKey(nameof(RoleId))]
    public Role Role { get; set; }
}

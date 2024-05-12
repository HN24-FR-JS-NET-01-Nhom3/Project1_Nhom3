using System.ComponentModel;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LotteryChecker.Core.Entities;

[Index(nameof(UserName), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class AppUser : IdentityUser<Guid>
{
    [StringLength(50)]
    public string? FirstName { get; set; }
    [Required]
    [StringLength(50)]
    public string? LastName { get; set; }
    public DateTime? LastLogin { get; set; }
        
    [DefaultValue(true)]
    public bool IsActive { get; set; }
}
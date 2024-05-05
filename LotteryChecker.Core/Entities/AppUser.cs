using System.ComponentModel;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.Core.Entities;

public class AppUser : IdentityUser<Guid>
{
    [StringLength(50)]
    public string? FirstName { get; set; }
        
    [StringLength(50)]
    public string? LastName { get; set; }
    public DateTime? LastLogin { get; set; }
        
    [DefaultValue(true)]
    public bool IsActive { get; set; }
}
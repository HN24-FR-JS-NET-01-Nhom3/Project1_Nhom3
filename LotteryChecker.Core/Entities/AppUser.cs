using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.Core.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}

namespace LotteryChecker.API.Models.Entities
{
    public class UserVm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? LastLogin { get; set; }     
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}

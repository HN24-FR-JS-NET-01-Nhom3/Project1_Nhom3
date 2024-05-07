namespace LotteryChecker.API.Models.Entities
{
    public class CreateUserVm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Phone { get; set; }
    }
}

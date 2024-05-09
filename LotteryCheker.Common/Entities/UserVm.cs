namespace LotteryChecker.Common.Entities
{
    public class UserVm
    {
<<<<<<<< HEAD:LotteryCheker.Common/Entities/UserVm.cs
        public Guid Id { get; set; }
========
        
>>>>>>>> 08b9439c12f14ef607354b63df35c8829766e144:LotteryChecker.API/Models/Entities/UserVm.cs
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}

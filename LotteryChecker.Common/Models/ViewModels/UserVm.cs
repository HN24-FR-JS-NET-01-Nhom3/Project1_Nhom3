<<<<<<<< HEAD:LotteryChecker.Common/Models/ViewModels/UserVm.cs
namespace LotteryChecker.Common.Models.ViewModels
========
﻿namespace LotteryChecker.Common.Entities
>>>>>>>> Viet:LotteryChecker.Common/Entities/UserVm.cs
{
    public class UserVm
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
<<<<<<<< HEAD:LotteryChecker.Common/Models/ViewModels/UserVm.cs
========
        public DateTime? LastLogin { get; set; }
>>>>>>>> Viet:LotteryChecker.Common/Entities/UserVm.cs
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}

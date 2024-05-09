using System.ComponentModel.DataAnnotations;

<<<<<<<< HEAD:LotteryChecker.Common/Models/Entities/RewardVm.cs
namespace LotteryChecker.Common.Models.Entities;
========
namespace LotteryChecker.Common.Entities;
>>>>>>>> Viet:LotteryCheker.Common/Entities/RewardVm.cs

public class RewardVm
{
	[Required(ErrorMessage = "Reward value is required")]
	public int RewardValue { get; set; }
	
	[StringLength(50)]
	public string? RewardName { get; set; }
}
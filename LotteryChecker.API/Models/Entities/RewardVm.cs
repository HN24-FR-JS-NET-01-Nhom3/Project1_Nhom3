using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.API.Models.Entities;

public class RewardVm
{
	[Required(ErrorMessage = "Reward value is required")]
	public int RewardValue { get; set; }
	
	[StringLength(50)]
	public string? RewardName { get; set; }
}
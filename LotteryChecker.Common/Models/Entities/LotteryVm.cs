using System.ComponentModel.DataAnnotations;

<<<<<<<< HEAD:LotteryChecker.Common/Models/Entities/LotteryVm.cs
namespace LotteryChecker.Common.Models.Entities;
========
namespace LotteryCheker.Common.Entities;
>>>>>>>> Viet:LotteryCheker.Common/Entities/LotteryVm.cs

public class LotteryVm
{
	public DateTime DrawDate { get; set; }
	public DateTime? PublishDate { get; set; }
	
	[Required(ErrorMessage = "Lottery number is required")]
	[RegularExpression(@"^\d{1,6}$", ErrorMessage = "Lottery number must be a 6-digit number")]
	public string LotteryNumber { get; set; }
	public bool IsPublished { get; set; }
	
	[StringLength(50)]
	public string? Company { get; set; }
	public int RewardId { get; set; }
	public string RewardName { get; set; }
	public int RewardValue { get; set; }
}
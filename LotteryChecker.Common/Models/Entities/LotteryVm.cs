using System.ComponentModel.DataAnnotations;
namespace LotteryChecker.Common.Models.Entities;

public class LotteryVm
{
    public int LotteryId { get; set; }
    public DateTime DrawDate { get; set; }
	
	[Required(ErrorMessage = "Lottery number is required")]
	[RegularExpression(@"^\d{1,6}$", ErrorMessage = "Lottery number must be from 1 to 6 digits number")]
	public string LotteryNumber { get; set; }
	public bool IsPublished { get; set; }
	
	[StringLength(50)]
	public string? Company { get; set; }
	public int RewardId { get; set; }
	public string? RewardName { get; set; }
	public int? RewardValue { get; set; }
}
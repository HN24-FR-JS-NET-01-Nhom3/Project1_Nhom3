using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.Common.Models.ViewModels;

public class CreateLotteryVm
{
	public DateTime DrawDate { get; set; }
	public DateTime? PublishDate { get; set; }
	
	[Required(ErrorMessage = "Lottery number is required")]
	[RegularExpression(@"^\d{1,6}$", ErrorMessage = "Lottery number must be from 1 to 6 digits number")]
	public string LotteryNumber { get; set; }
	
	[DefaultValue(true)]
	public bool IsPublished { get; set; }
	
	[StringLength(50)]
	public string? Company { get; set; }
	
	public int? RewardId { get; set; }
}
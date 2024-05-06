using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.MVC.Models.Entities;

public class LotteryVm
{
	public DateTime DueDate { get; set; }
	public DateTime? PublishDate { get; set; }
	
	[Required(ErrorMessage = "Lottery number is required")]
	[RegularExpression(@"^\d{5}$", ErrorMessage = "Lottery number must be a 5-digit number")]
	public int LotteryNumber { get; set; }
	public bool IsPublished { get; set; }
	
	[StringLength(50)]
	public string? Company { get; set; }
	public int RewardId { get; set; }
}
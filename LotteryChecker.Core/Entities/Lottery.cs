using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.Core.Entities;

public class Lottery
{
	public int LotteryId { get; set; }
	
	[Required(ErrorMessage = "Drawing date is required")]
	public DateTime DrawDate { get; set; }
	public DateTime? PublishDate { get; set; }
	
	[Required(ErrorMessage = "Lottery number is required")]
    [RegularExpression(@"^\d{1,6}$", ErrorMessage = "Lottery number must be a 6-digit number")]
    public string LotteryNumber { get; set; }
    
    [DefaultValue(true)]
	public bool IsPublished { get; set; }
	
	[StringLength(50)]
	public string? Company { get; set; }
	public int RewardId { get; set; }
	public Reward? Reward { get; set; }
} 
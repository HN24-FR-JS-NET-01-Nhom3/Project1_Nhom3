using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace LotteryChecker.Core.Entities;

public class Lottery
{
	public int LotteryId { get; set; }
	[Required(ErrorMessage = "Due date is required")]
	public DateTime DueDate { get; set; }
	public DateTime? PublishDate { get; set; }
	[Required(ErrorMessage = "Lottery number is required")]
    [RegularExpression(@"^\d{6}$", ErrorMessage = "Lottery number must be a 6-digit number")]
    public int LotteryNumber { get; set; }
	public bool IsPublished { get; set; }
	public string? Company { get; set; }
	public Reward? Reward { get; set; }
	[ForeignKey("RewardId")]
    public int RewardId { get; set; }
} 
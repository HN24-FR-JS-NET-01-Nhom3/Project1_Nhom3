using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.Core.Entities;

public class Reward
{
    public int RewardId { get; set; }
        
    [Required(ErrorMessage = "Reward value is required")]
    public int RewardValue { get; set; }
    [StringLength(50)]
    public string? RewardName { get; set; }
    public IList<Lottery>? Lotteries { get; set; }
}
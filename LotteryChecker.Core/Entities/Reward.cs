using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.Core.Entities
{
    public class Reward
    {
        public int RewardId { get; set; }
        [Required(ErrorMessage = "Reward Value is required")]
        public decimal RewardValue { get; set; }
        public string? RewardName { get; set; }
        public IList<Lottery>? Lotteries { get; set; }
    }
}
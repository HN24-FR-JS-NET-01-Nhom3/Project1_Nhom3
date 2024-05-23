using System.ComponentModel.DataAnnotations;
using LotteryChecker.Common.Validation;

namespace LotteryChecker.Common.Models.Entities
{
    public class SearchHistoryVm
    {
        public int SearchHistoryId { get; set; }
        [Required(ErrorMessage = "Lottery number is required")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Lottery number must be a 6-digit number")]
        public string LotteryNumber { get; set; }
        public int? Prize { get; set; } = 0;

        [Required(ErrorMessage = "Draw date is required")]
        public DateTime DrawDate { get; set; }
        
        [Required(ErrorMessage = "Search date is required")]
        public DateTime SearchDate { get; set; }
        public Guid UserId { get; set; }
        public string? Email { get; set; }
    }
}

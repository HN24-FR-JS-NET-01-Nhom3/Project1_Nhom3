using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.Common.Models.ViewModels
{
    public class CreateSearchHistoryVm
    {
        [Required(ErrorMessage = "Lottery number is required")]
        [RegularExpression(@"^\d{1,6}$", ErrorMessage = "Lottery number must be a 6-digit number")]
        public string LotteryNumber { get; set; }
        public int? Prize { get; set; } = 0;

        [Required(ErrorMessage = "Draw date is required")]
        public DateTime DrawDate { get; set; }

        [Required(ErrorMessage = "Search date is required")]
        public DateTime SearchDate { get; set; }
        public Guid UserId { get; set; }
    }
}

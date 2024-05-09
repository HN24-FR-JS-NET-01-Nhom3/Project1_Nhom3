using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.MVC.Models.ViewModels
{
    public class SearchHistoryVm
    {
        [Required(ErrorMessage = "Lottery number is required")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Lottery number must be a 6-digit number")]
        public string LotteryNumber { get; set; }

        [Required(ErrorMessage = "Search date is required")]
        public DateTime SearchDate { get; set; }
        public Guid UserId { get; set; }
    }
}

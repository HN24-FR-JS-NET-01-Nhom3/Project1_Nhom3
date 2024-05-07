using LotteryChecker.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.API.Models.Entities
{
    public class SearchHistoryVm
    {
        [Required(ErrorMessage = "Lottery number is required")]
        [RegularExpression(@"^\d{1,6}$", ErrorMessage = "Lottery number must be a 6-digit number")]
        public string LotteryNumber { get; set; }

        [Required(ErrorMessage = "Search date is required")]
        public DateTime SearchDate { get; set; }
        public Guid UserId { get; set; }
    }
}

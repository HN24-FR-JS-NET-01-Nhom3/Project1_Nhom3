using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.Core.Entities
{
    public class SearchHistory
    {
        [Key]
        public int SearchId { get; set; }
        [Required(ErrorMessage = "Lottery number is required")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Lottery number must be a 6-digit number")]
        public int LotteryNumber { get; set; }
        [Required(ErrorMessage = "Search Date is required")]
        public DateTime SearchDate { get; set; }
        public Guid UserId { get; set; }

    }
}

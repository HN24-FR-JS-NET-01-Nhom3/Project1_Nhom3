using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LotteryChecker.Core.Entities
{
    public class PurchaseTicket
    {
        public int PurchaseTicketId { get; set; }
        public DateTime PurchaseDate { get; set; }
        [Required(ErrorMessage = "Lottery number is required")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Lottery number must be a 6-digit number")]
        public int LotteryNumber { get; set; }
        [Required(ErrorMessage = "User is required")]
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
    }
}

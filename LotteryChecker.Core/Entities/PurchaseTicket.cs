using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.Core.Entities;

public class PurchaseTicket
{
    public int PurchaseTicketId { get; set; }
        
    [Required(ErrorMessage = "Purchase date is required")]
    public DateTime PurchaseDate { get; set; }
        
    [Required(ErrorMessage = "Lottery number is required")]
    [RegularExpression(@"^\d{1,6}$", ErrorMessage = "Lottery number must be a 6-digit number")]
    public string LotteryNumber { get; set; }
        
    [Required(ErrorMessage = "User is required")]
    public Guid UserId { get; set; }
        
    public AppUser User { get; set; }
}
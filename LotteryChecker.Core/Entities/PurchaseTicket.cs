using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.Core.Entities;

public class PurchaseTicket
{
    public int? PurchaseTicketId { get; set; }
    public DateTime? PurchaseDate { get; set; }
        
    [Required(ErrorMessage = "Lottery number is required")]
    [RegularExpression(@"^\d{6}$", ErrorMessage = "Lottery number must be a 6-digit number")]
    public string LotteryNumber { get; set; }
    
    [Required(ErrorMessage = "Draw date is required")]
    public DateTime DrawDate { get; set; }
    public Guid? UserId { get; set; }
        
    public AppUser? User { get; set; }
}
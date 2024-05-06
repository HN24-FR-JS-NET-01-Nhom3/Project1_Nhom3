using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.API.Models.Entities;

public class PurchaseTicketVm
{
	[Required(ErrorMessage = "Purchase date is required")]
	public DateTime PurchaseDate { get; set; }
        
	[Required(ErrorMessage = "Lottery number is required")]
	[RegularExpression(@"^\d{5}$", ErrorMessage = "Lottery number must be a 5-digit number")]
	public int LotteryNumber { get; set; }
        
	[Required(ErrorMessage = "User is required")]
	public Guid UserId { get; set; }
}
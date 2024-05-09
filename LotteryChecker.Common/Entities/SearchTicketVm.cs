using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.Common.Entities;

public class SearchTicketVm
{
	[Required(ErrorMessage = "Ticket number is required")]
	[RegularExpression(@"^\d{1,6}$", ErrorMessage = "Lottery number must be a 6-digit number")]
	public string TicketNumber { get; set; }
	
	[Required(ErrorMessage = "Search date is required")]
	public DateTime DrawDate { get; set; }
}
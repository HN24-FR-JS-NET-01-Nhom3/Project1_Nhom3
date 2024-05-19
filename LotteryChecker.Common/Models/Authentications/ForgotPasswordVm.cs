using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.Common.Models.Authentications;

public class ForgotPasswordVm
{
	[Required]
	[EmailAddress]
	public string Email { get; set; }
}
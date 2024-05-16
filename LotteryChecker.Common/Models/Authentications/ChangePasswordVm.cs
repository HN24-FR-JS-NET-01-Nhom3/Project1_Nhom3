using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.Common.Models.Authentications;

public class ChangePasswordVm
{
	[Required(ErrorMessage = "Nhập mật khẩu hiện tại")]
	[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
	[DataType(DataType.Password)]
	public string CurrentPassword { get; set; }

	[Required(ErrorMessage = "Nhập mật khẩu mới")]
	[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
	[DataType(DataType.Password)]
	public string NewPassword { get; set; }

	[Required(ErrorMessage = "Xác nhận mật khẩu mới")]
	[Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
	public string ConfirmPassword { get; set; }
}
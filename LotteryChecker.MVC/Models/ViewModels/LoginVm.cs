using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.MVC.Models.ViewModels;

public class LoginVm
{
	[Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
	[DataType(DataType.Text)]
	[Display(Name = "Tên đăng nhập")]
	public string Email { get; set; }
	
	[Required(ErrorMessage = "Mật khẩu không được để trống.")]
	[DataType(DataType.Password)]
	[Display(Name = "Mật khẩu")]
	public string Password { get; set; }
}
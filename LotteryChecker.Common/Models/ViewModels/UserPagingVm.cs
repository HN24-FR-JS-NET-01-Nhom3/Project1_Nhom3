using LotteryChecker.Common.Models.Authentications;

namespace LotteryChecker.Common.Models.ViewModels;

public class UserPagingVm
{
	public IEnumerable<UserVm> Result { get; set; }
	public Meta Meta { get; set; }
}
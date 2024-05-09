

using LotteryChecker.Common.Entities;

namespace LotteryChecker.MVC.Models.ViewModels;

public class LotteriesVm
{
	public IEnumerable<IGrouping<int, LotteryVm>>? LotteryVmGroups { get; set; }
	public IEnumerable<RewardVm>? RewardVms { get; set; }
	public DateTime? CurrentDate { get; set; }
}
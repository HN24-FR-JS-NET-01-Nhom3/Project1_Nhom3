using System.Collections;

namespace LotteryChecker.Common.Models.ViewModels;

public class AdminStatisticVm
{
	public int UserCount { get; set; }
	public int RewardCount { get; set; }
	public int LotteryCount { get; set; }
	public int PurchaseCount { get; set; }
	
	public IEnumerable<AdminMonthlyStatisticVm> MonthlyStatistic { get; set; }
}
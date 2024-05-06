using AutoMapper;
using LotteryChecker.Core.Entities;
using LotteryChecker.MVC.Models.Entities;

namespace LotteryChecker.MVC.AutoMapper;

public class AutoMapper : Profile
{
	public AutoMapper()
	{
		CreateMap<Lottery, LotteryVm>().ReverseMap();
		CreateMap<PurchaseTicket, PurchaseTicketVm>().ReverseMap();
		CreateMap<Reward, RewardVm>().ReverseMap();
	}
}
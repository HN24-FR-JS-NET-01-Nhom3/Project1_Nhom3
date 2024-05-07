using AutoMapper;
using LotteryChecker.API.Models.Entities;
using LotteryChecker.Core.Entities;

namespace LotteryChecker.API.AutoMapper;

public class AutoMapper : Profile
{
	public AutoMapper()
	{
		CreateMap<Lottery, LotteryVm>().ReverseMap();
		CreateMap<PurchaseTicket, PurchaseTicketVm>().ReverseMap();
		CreateMap<Reward, RewardVm>().ReverseMap();
		CreateMap<AppUser, UserVm>().ReverseMap();
	}
}
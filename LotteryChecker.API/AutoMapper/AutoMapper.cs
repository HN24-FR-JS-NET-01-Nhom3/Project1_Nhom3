using AutoMapper;
using LotteryChecker.API.Models.Entities;
using LotteryChecker.Core.Entities;

namespace LotteryChecker.API.AutoMapper;

public class AutoMapper : Profile
{
	public AutoMapper()
	{
		CreateMap<Lottery, LotteryVm>()
			.ForMember(dest => dest.RewardName, opt => opt.MapFrom(src => src.Reward.RewardName))
			.ForMember(dest => dest.RewardValue, opt => opt.MapFrom(src => src.Reward.RewardValue))
			.ReverseMap();
		CreateMap<PurchaseTicket, PurchaseTicketVm>().ReverseMap();
		CreateMap<Reward, RewardVm>().ReverseMap();
	}
}
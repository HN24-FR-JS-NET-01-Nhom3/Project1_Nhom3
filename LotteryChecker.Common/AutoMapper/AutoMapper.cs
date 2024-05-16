using AutoMapper;
using LotteryChecker.Common.Models.Authentications;
using LotteryChecker.Common.Models.Entities;
using LotteryChecker.Common.Models.ViewModels;
using LotteryChecker.Core.Entities;

namespace LotteryChecker.Common.AutoMapper
{
    public class MyAutoMapper : Profile
    {
        public MyAutoMapper()
        {
            CreateMap<Lottery, LotteryVm>()
                .ForMember(dest => dest.RewardName, opt => opt.MapFrom(src => src.Reward.RewardName))
                .ForMember(dest => dest.RewardValue, opt => opt.MapFrom(src => src.Reward.RewardValue))
                .ReverseMap();
            CreateMap<Lottery, CreateLotteryVm>().ReverseMap();
            CreateMap<PurchaseTicket, PurchaseTicketVm>().ReverseMap();
            CreateMap<SearchHistory, SearchHistoryVm>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ReverseMap();
            CreateMap<SearchHistory, CreateSearchHistoryVm>().ReverseMap();
            CreateMap<Reward, RewardVm>().ReverseMap();
            CreateMap<AppUser, UserVm>().ReverseMap();
            CreateMap<AppUser, RegisterVm>().ReverseMap();
        }
    }
}

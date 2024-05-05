using LotteryChecker.Core.Data;
using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Infrastructures;
using LotteryChecker.Core.IRepositories;

namespace LotteryChecker.Core.Repositories;

public class RewardRepository : BaseRepository<Reward>, IRewardRepository
{
    public RewardRepository(LotteryContext context) : base(context)
    {
        }
}
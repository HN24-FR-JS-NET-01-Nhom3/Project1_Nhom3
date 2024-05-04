using LotteryChecker.Core.Data;
using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Infrastructures;
using LotteryChecker.Core.IRepositories;

namespace LotteryChecker.Core.Repositories
{
    public class LotteryRepository : BaseRepository<Lottery>, ILotteryRepository
    {
        public LotteryRepository(LotteryContext context) : base(context)
        {
        }
    }
}

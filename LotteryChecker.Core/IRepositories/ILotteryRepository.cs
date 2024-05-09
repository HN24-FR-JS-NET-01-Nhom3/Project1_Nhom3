using System.Collections;
using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Infrastructures;

namespace LotteryChecker.Core.IRepositories;

public interface ILotteryRepository : IBaseRepository<Lottery>
{
    public IEnumerable<Lottery> GenerateLotteryResult(DateTime dateTime);
    public IEnumerable<Lottery> GetLotteryResult(DateTime dateTime);
    public void UnpublishExpiredLotteries();
}
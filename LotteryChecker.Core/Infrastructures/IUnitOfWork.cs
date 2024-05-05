using LotteryChecker.Core.Data;
using LotteryChecker.Core.IRepositories;

namespace LotteryChecker.Core.Infrastructures;

public interface IUnitOfWork : IDisposable
{
    public ILotteryRepository LotteryRepository { get; }
    public IPurchaseTicketRepository PurchaseTicketRepository { get; }
    public IRewardRepository RewardRepository { get; }
    public ISearchHistoryRepository SearchHistoryRepository { get; }
    public IUserRepository UserRepository { get; }
    public LotteryContext Context { get; }
    int SaveChanges();
}
using LotteryChecker.Core.Data;
using LotteryChecker.Core.IRepositories;
using LotteryChecker.Core.Repositories;

namespace LotteryChecker.Core.Infrastructures;

public class UnitOfWork : IUnitOfWork
{
    private readonly LotteryContext _context;
    private ILotteryRepository? _lotteryRepository;
    private IPurchaseTicketRepository? _purchaseTicketRepository;    
    private IRewardRepository? _rewardRepository;
    private ISearchHistoryRepository? _searchHistoryRepository;
    private IUserRepository? _userRepository;

    public UnitOfWork(LotteryContext context)
    {
        _context = context;
    }

    public ILotteryRepository LotteryRepository => _lotteryRepository ?? (_lotteryRepository = new LotteryRepository(_context));

    public IPurchaseTicketRepository PurchaseTicketRepository => _purchaseTicketRepository ?? (_purchaseTicketRepository = new PurchaseTicketRepository(_context));

    public IRewardRepository RewardRepository => _rewardRepository ?? (_rewardRepository = new RewardRepository(_context));

    public ISearchHistoryRepository SearchHistoryRepository => _searchHistoryRepository ?? (_searchHistoryRepository = new SearchHistoryRepository(_context));

    public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(_context));

    public LotteryContext Context => _context;

    public void Dispose()
    {
        _context.Dispose();
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }
}
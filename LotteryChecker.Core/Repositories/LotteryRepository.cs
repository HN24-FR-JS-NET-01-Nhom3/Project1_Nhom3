using LotteryChecker.Core.Data;
using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Infrastructures;
using LotteryChecker.Core.IRepositories;

namespace LotteryChecker.Core.Repositories;

public class LotteryRepository : BaseRepository<Lottery>, ILotteryRepository
{
    public LotteryRepository(LotteryContext context) : base(context)
    {
           
    }

    public IList<Lottery> GetLotteriesByCompany(string company)
    {
        return Find(x => x.Company == company).ToList();
    }

    public IList<Lottery> GetLotteriesByDate(DateTime dateStart, DateTime dateEnd)
    {
        return Find(x => x.PublishDate >= dateStart && x.PublishDate <= dateEnd).ToList();
    }

    public IList<Lottery> GetLotteriesByDateAndCompany(DateTime dateStart, DateTime dateEnd, string company)
    {
        return Find(x => x.PublishDate >= dateStart && x.PublishDate <= dateEnd && x.Company == company).ToList();
    }

    public IList<Lottery> GetLotteriesExpired()
    {
        return Find(x => x.DueDate < DateTime.Now).ToList();
    }

    public IList<Lottery> GetLotteriesPublished()
    {
        return Find(x => x.IsPublished).ToList();
    }

    public IList<Lottery> GetLotteriesUnpublished()
    {
        return Find(x => x.IsPublished == false).ToList();
    }
}
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

        public IList<Lottery> GetLotteriesByCompany(string company)
        {
            return Context.Lotteries.Where(x => x.Company == company).ToList();
        }

        public IList<Lottery> GetLotteriesByDate(DateTime dateSart, DateTime dateEnd)
        {
            return Context.Lotteries.Where(x => x.PublishDate >= dateSart && x.PublishDate <= dateEnd).ToList();
        }

        public IList<Lottery> GetLotteriesByDateAndCompany(DateTime dateSart, DateTime dateEnd, string company)
        {
            return Context.Lotteries.Where(x => x.PublishDate >= dateSart && x.PublishDate <= dateEnd && x.Company == company).ToList();
        }

        public IList<Lottery> GetLotteriesExpired()
        {
            return Context.Lotteries.Where(x => x.DueDate < DateTime.Now).ToList();
        }

        public IList<Lottery> GetLotteriesPublished()
        {
            return Context.Lotteries.Where(x => x.IsPublished == true).ToList();
        }

        public IList<Lottery> GetLotteriesUnpublished()
        {
            return Context.Lotteries.Where(x => x.IsPublished == false).ToList();
        }
    }
}

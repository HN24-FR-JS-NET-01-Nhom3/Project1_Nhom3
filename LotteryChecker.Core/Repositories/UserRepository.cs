using LotteryChecker.Core.Data;
using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Enums;
using LotteryChecker.Core.Infrastructures;
using LotteryChecker.Core.IRepositories;

namespace LotteryChecker.Core.Repositories;

public class UserRepository : BaseRepository<AppUser>, IUserRepository
{
    public UserRepository(LotteryContext context) : base(context)
    {
    }
    public AppUser? FindUserByPhone(string phone)
    {
        return Find(x => x.PhoneNumber == phone).FirstOrDefault();
    }
    public AppUser? FindUserByEmail(string email)
    {
        return Find(x => x.Email == email).FirstOrDefault();
    }

    public List<AppUser> FilterUserIsActive(int timeFrame, TimeUnit unit)
    {
        DateTime currentTime = DateTime.Now;
        DateTime threshold = currentTime.AddYears(-1 * timeFrame);
        switch (unit)
        {
            case TimeUnit.Week:
                threshold = currentTime.AddDays(-7 * timeFrame);
                break;
            case TimeUnit.Month:
                threshold = currentTime.AddMonths(-1 * timeFrame);
                break;
            case TimeUnit.Quarter:
                threshold = currentTime.AddMonths(-3 * timeFrame);
                break;
            case TimeUnit.Year:
                threshold = currentTime.AddYears(-1 * timeFrame);
                break;
        }
        var inactiveUsers = Find(user => user.LastLogin < threshold).ToList();
        return inactiveUsers;
    }
}
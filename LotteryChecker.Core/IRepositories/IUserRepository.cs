using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Enums;
using LotteryChecker.Core.Infrastructures;

namespace LotteryChecker.Core.IRepositories
{
    public interface IUserRepository :  IBaseRepository<AppUser>
    {
        public AppUser FindUserByPhone(string phone);
        public AppUser FindUserByEmail(string email);
        //Lọc các người dùng đã không đăng nhập vào hệ thống trong thời gian nhất định do người dùng chọn (ví dụ 1 tuần, 1 tháng, 1 quý hoặc 1 năm)
        List<AppUser> FilterUserIsActive(int timeFrame, TimeUnit unit);
    }
}

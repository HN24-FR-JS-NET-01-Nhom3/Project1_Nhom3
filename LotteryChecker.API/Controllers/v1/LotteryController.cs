using LotteryChecker.Core.Data;
using LotteryChecker.Core.Infrastructures;
using Microsoft.AspNetCore.Mvc;

namespace LotteryChecker.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotteryController : ControllerBase
    {
        private readonly LotteryContext _context;
        private readonly IUnitOfWork _unitOfWork;
    }
}

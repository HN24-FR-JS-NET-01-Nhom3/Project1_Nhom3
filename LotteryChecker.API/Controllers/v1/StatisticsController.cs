using Asp.Versioning;
using AutoMapper;
using LotteryChecker.Common.Models.Entities;
using LotteryChecker.Common.Models.Http;
using LotteryChecker.Core.Infrastructures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LotteryChecker.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StatisticsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet("get-statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                var userId = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var currentYear = DateTime.Now.Year;

                // Tạo danh sách các tháng trong năm hiện tại với giá trị mặc định
                var months = Enumerable.Range(1, 12).Select(month => new MonthlyStatistics
                {
                    Year = currentYear,
                    Month = month,
                    Count = 0,
                    SumPrize = 0
                }).ToList();

                var userStatistics = await _unitOfWork.Context.SearchHistories
                    .Where(t => t.UserId == userId && t.SearchDate.Year == currentYear)
                    .GroupBy(t => new { t.SearchDate.Year, t.SearchDate.Month })
                    .Select(g => new MonthlyStatistics
                    {
                        Year = g.Key.Year,
                        Month = g.Key.Month,
                        Count = g.Count(),
                        SumPrize = g.Sum(t => t.Prize)
                    })
                    .ToListAsync();

                // Kết hợp dữ liệu từ cơ sở dữ liệu với danh sách các tháng
                foreach (var stat in userStatistics)
                {
                    var monthStat = months.FirstOrDefault(m => m.Month == stat.Month);
                    if (monthStat != null)
                    {
                        monthStat.Count = stat.Count;
                        monthStat.SumPrize = stat.SumPrize;
                    }
                }

                return Ok(months);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Errors = new[] { ex.Message }
                });
            }
        }
    }
}

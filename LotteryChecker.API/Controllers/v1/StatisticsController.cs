using Asp.Versioning;
using AutoMapper;
using LotteryChecker.Common.Models.Entities;
using LotteryChecker.Common.Models.Http;
using LotteryChecker.Common.Models.ViewModels;
using LotteryChecker.Core.Infrastructures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Security.Claims;

namespace LotteryChecker.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/statistic")]
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
                        SumPrize = g.Sum(t => (long?)t.Prize ?? 0)
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
                var response = new
                {
                    Data = new
                    {
                        Result = months,
                        Meta = (object)null
                    }
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Errors = new[] { ex.Message }
                });
            }
        }

        [HttpGet("get-count-user")]
        public async Task<IActionResult> GetCountUser()
        {
            try
            {
                var countUser = await _unitOfWork.Context.Users.CountAsync();
                var response = new 
                {
                    Data = new 
                    {
                        Result = countUser,
                        Meta = (object)null
                    }
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Errors = new[] { ex.Message
                    }
                });
            }
        }

        [HttpGet("get-count-lottery")]
        public async Task<IActionResult> GetCountLottery()
        {
            try
            {
                var countLottery = await _unitOfWork.Context.Lotteries.CountAsync();
                var response = new
                {
                    Data = new
                    {
                        Result = countLottery,
                        Meta = (object)null
                    }
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Errors = new[] { ex.Message
                    }
                });
            }
        }

        [HttpGet("get-count-purchase-ticket")]
        public async Task<IActionResult> GetCountPurchaseTicket()
        {
            try
            {
                var countPurchaseTicket = await _unitOfWork.Context.PurchaseTickets.CountAsync();
                var response = new
                {
                    Data = new
                    {
                        Result = countPurchaseTicket,
                        Meta = (object)null
                    }
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Errors = new[] { ex.Message
                    }
                });
            }
        }

        [HttpGet("get-winner")]
        public async Task<IActionResult> GetWinner()
        {
            try
            {   
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
                    .Where(t => t.SearchDate.Year == currentYear)
                    .GroupBy(t => new { t.SearchDate.Year, t.SearchDate.Month })
                    .Select(g => new MonthlyStatistics
                    {
                        Year = g.Key.Year,
                        Month = g.Key.Month,
                        Count = g.Count(),
                        SumPrize = g.Sum(t => (long?)t.Prize ?? 0)
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
                var response = new
                {
                    Data = new
                    {
                        Result = months,
                        Meta = (object)null
                    }
                };
                return Ok(response);
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

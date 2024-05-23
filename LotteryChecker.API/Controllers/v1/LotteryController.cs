using Asp.Versioning;
using AutoMapper;
using LotteryChecker.API.Helpers;
using LotteryChecker.Common.Models.Entities;
using LotteryChecker.Common.Models.Http;
using LotteryChecker.Common.Models.ViewModels;
using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Infrastructures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace LotteryChecker.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/lottery")]
public class LotteryController : ControllerBase
{
	private readonly IUnitOfWork _unitOfWork;
	private IMapper _mapper;
	private UserManager<AppUser> _userManager;

	public LotteryController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_userManager = userManager;
	}

	[HttpGet("get-all-lotteries/page={page}&pageSize={pageSize}")]
	public IActionResult GetAllLotteries([FromQuery] LotteryQuery query, int page = 1, int pageSize = 17)
	{
		try
		{
			var lotteries = _unitOfWork.LotteryRepository.GetAll().OrderByDescending(l => l.DrawDate.Date)
				.ThenBy(l => l.RewardId).ToList();
			if (!lotteries.IsNullOrEmpty())
			{
				lotteries = query.Filters
					.Aggregate(lotteries, (current, filter) => current.Where(filter).ToList());
				var lotteryPaging = _unitOfWork.LotteryRepository
					.GetPaging(lotteries, null, page, pageSize).ToList();

				var response = new Response<LotteryVm>()
				{
					Data = new Data<LotteryVm>()
					{
						Result = lotteryPaging.Select(lottery => _mapper.Map<LotteryVm>(lottery)),
						Meta = new Meta()
						{
							Page = page,
							PageSize = pageSize > lotteryPaging.Count ? lotteryPaging.Count : pageSize,
							TotalPages = (int)Math.Ceiling((decimal)lotteries.Count / pageSize)
						}
					}
				};

				return Ok(response);
			}

			return NotFound(new Response<LotteryVm>()
			{
				Errors = new[] { "No lotteries found" }
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new Response<LotteryVm>()
			{
				Errors = new[] { ex.Message }
			});
		}
	}

	[HttpGet("get-lottery/{id}")]
	public IActionResult GetLotteryById(int id)
	{
		try
		{
			var lottery = _unitOfWork.LotteryRepository.GetById(id);
			if (lottery == null)
				return NotFound(new Response<LotteryVm>()
				{
					Errors = new[] { "No lotteries found" }
				});
			var lotteryVm = _mapper.Map<LotteryVm>(lottery);
			return Ok(new Response<LotteryVm>()
			{
				Data = new Data<LotteryVm>()
				{
					Result = [lotteryVm]
				}
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new Response<LotteryVm>()
			{
				Errors = new[] { ex.Message }
			});
		}
	}

	[HttpPost("create-lottery")]
	public IActionResult CreateLottery([FromBody] CreateLotteryVm lotteryVm)
	{
		try
		{
			var lottery = _mapper.Map<Lottery>(lotteryVm);
			lottery.IsPublished = false;
			lottery.PublishDate = DateTime.MinValue;
			_unitOfWork.LotteryRepository.Create(lottery);
			int result = _unitOfWork.SaveChanges();
			if (result > 0)
				return Ok(new Response<Lottery>()
				{
					Data = new Data<Lottery>()
					{
						Result = [lottery]
					}
				});
			return BadRequest(new Response<Lottery>()
			{
				Errors = new[] { "Error happened when saving lottery to database" }
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new Response<Lottery>()
			{
				Errors = new[] { ex.Message }
			});
		}
	}

	[HttpPut("update-lottery/{id}")]
	public IActionResult UpdateLottery([FromBody] LotteryVm lotteryVm)
	{
		try
		{
			var lottery = _mapper.Map<Lottery>(lotteryVm);
			lottery.Reward = null;
			lottery.DrawDate = lottery.DrawDate;
			_unitOfWork.LotteryRepository.Update(lottery);
			int result = _unitOfWork.SaveChanges();
			if (result > 0)
				return Ok(new Response<Lottery>()
				{
					Data = new Data<Lottery>()
					{
						Result = [lottery]
					}
				});
			return BadRequest(new Response<Lottery>()
			{
				Errors = new[] { "Error happened when saving lottery to database" }
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new Response<Lottery>()
			{
				Errors = new[] { ex.Message }
			});
		}
	}

	[HttpDelete("delete-lottery")]
	public IActionResult DeleteLottery(int id)
	{
		try
		{
			var lottery = _unitOfWork.LotteryRepository.GetById(id);
			if (lottery == null)
			{
				return NotFound();
			}

			_unitOfWork.LotteryRepository.Delete(id);
			int result = _unitOfWork.SaveChanges();
			if (result > 0)
				return Ok(new Response<Lottery>()
				{
					Message = "Lottery deleted successfully!"
				});
			return BadRequest(new Response<Lottery>()
			{
				Errors = new[] { "Error happened when deleting lottery to database" }
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new Response<Lottery>()
			{
				Errors = new[] { ex.Message }
			});
		}
	}

	[HttpPost("generate-lottery-result")]
	public IActionResult GenerateLotteryResult()
	{
		try
		{
			var lotteries = _unitOfWork.LotteryRepository.GenerateLotteryResult(DateTime.Now);
			var result = _unitOfWork.SaveChanges();
			if (result > 0)
			{
				return Ok(new Response<Lottery>()
				{
					Data = new Data<Lottery>()
					{
						Result = lotteries
					}
				});
			}

			return BadRequest(new Response<Lottery>()
			{
				Errors = new[] { "Cannot generate results!" }
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new Response<Lottery>()
			{
				Errors = new[] { ex.Message }
			});
		}
	}

	[HttpGet("get-lottery-result")]
	public IActionResult GetLotteryResult(int? year, int? month, int? day)
	{
		var dateTime = DateTime.Now;
		if (year != null && month != null && day != null)
		{
			dateTime = new DateTime((int)year, (int)month, (int)day);
		}

		IEnumerable<Lottery> lotteryResult = _unitOfWork.LotteryRepository.GetLotteryResult(dateTime);
		if (DateTime.Now.Hour < 19 && (DateTime.Now.Day == day || day == null))
		{
			lotteryResult = _unitOfWork.LotteryRepository.GetLotteryResult(dateTime.AddDays(-1));
		}

		if (DateTime.Now < dateTime)
		{
			lotteryResult = _unitOfWork.LotteryRepository.GetLotteryResult(DateTime.Now);
		}

		if (lotteryResult.IsNullOrEmpty())
		{
			var latestLottery = _unitOfWork.LotteryRepository.GetAll().FirstOrDefault();
			if (latestLottery != null)
			{
				lotteryResult = _unitOfWork.LotteryRepository.GetLotteryResult(latestLottery.DrawDate);
			}
		}

		return Ok(new Response<LotteryVm>()
		{
			Data = new Data<LotteryVm>()
			{
				Result = _mapper.Map<List<LotteryVm>>(lotteryResult)
			}
		});
	}

	[HttpPost("get-ticket-result")]
	public IActionResult GetTicketResult([FromBody] SearchHistoryVm searchHistoryVm)
	{
		var lotteries = _unitOfWork.LotteryRepository.GetLotteryResult(searchHistoryVm.DrawDate).ToList();
		lotteries = lotteries.OrderBy(l => l.RewardId).ToList();
		foreach (var lottery in lotteries)
		{
			if (searchHistoryVm.LotteryNumber.EndsWith(lottery.LotteryNumber))
			{
				var reward = _unitOfWork.RewardRepository.GetById(lottery.RewardId);
				return Ok(new Response<RewardVm>()
				{
					Data = new Data<RewardVm>()
					{
						Result = [_mapper.Map<RewardVm>(reward)]
					}
				});
			}
		}

		try
		{
			var specialPriceLottery = lotteries.First(l => l.RewardId == 1);
			if (searchHistoryVm.LotteryNumber.EndsWith(specialPriceLottery.LotteryNumber.Substring(1)))
			{
				var reward = _unitOfWork.RewardRepository.GetById(9);
				return Ok(new Response<RewardVm>()
				{
					Data = new Data<RewardVm>()
					{
						Result = [_mapper.Map<RewardVm>(reward)]
					}
				});
			}

			var countDuplicate = specialPriceLottery.LotteryNumber
				.Where((t, i) => searchHistoryVm.LotteryNumber[i] == t)
				.Count();
			if (countDuplicate == 5)
			{
				var reward = _unitOfWork.RewardRepository.GetById(10);
				return Ok(new Response<RewardVm>()
				{
					Data = new Data<RewardVm>()
					{
						Result = [_mapper.Map<RewardVm>(reward)]
					}
				});
			}

			return NotFound(new Response<Lottery>()
			{
				Errors = new[] { "Not found!" }
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new Response<Lottery>()
			{
				Errors = new[] { ex.Message }
			});
		}
	}

	[HttpPost("get-multiple-ticket-results")]
	public async Task<IActionResult> GetMultipleTicketResults(
		[FromBody] MultipleLotteryNumbersVm multipleLotteryNumbersVm)
	{
		try
		{
			var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
			}

			var user = await _userManager.FindByIdAsync(userId);

			var lotteryNumbers = multipleLotteryNumbersVm.LotteryNumbers;
			var lotteries = _unitOfWork.LotteryRepository.GetLotteryResult(multipleLotteryNumbersVm.DrawDate).ToList();
			var results = new Dictionary<string, RewardVm?>();
			foreach (var number in lotteryNumbers)
			{
				var searchHistoryVm = new SearchHistoryVm
				{
					SearchDate = DateTime.Now,
					LotteryNumber = number.Trim(),
					DrawDate = multipleLotteryNumbersVm.DrawDate,
					Email = user.Email,
					UserId = Guid.Parse(userId)
				};
				var result = GetTicketResult(searchHistoryVm);

				if (result is OkObjectResult okResult)
				{
					var rewardVm = (okResult.Value as Response<RewardVm>)?.Data?.Result?.FirstOrDefault();
					results.Add(number, rewardVm);
				}
				else
				{
					results.Add(number, null);
				}

				var specialPriceLottery = lotteries.First(l => l.RewardId == 1);
				if (searchHistoryVm.LotteryNumber.EndsWith(specialPriceLottery.LotteryNumber.Substring(1)))
				{
					var reward = _unitOfWork.RewardRepository.GetById(9);
					if (!results.ContainsKey(number))
					{
						results.Add(number, _mapper.Map<RewardVm>(reward));
					}
				}

				var countDuplicate = specialPriceLottery.LotteryNumber
					.Where((t, i) => searchHistoryVm.LotteryNumber[i] == t)
					.Count();
				if (countDuplicate == 5)
				{
					var reward = _unitOfWork.RewardRepository.GetById(10);
					if (!results.ContainsKey(number))
					{
						results.Add(number, _mapper.Map<RewardVm>(reward));
					}
				}
			}

			return Ok(new Response<Dictionary<string, RewardVm?>>
				{ Data = new Data<Dictionary<string, RewardVm?>> { Result = [results] } });
		}
		catch (Exception ex)
		{
			return BadRequest(new Response<Dictionary<string, RewardVm?>> { Errors = new[] { ex.Message } });
		}
	}

	[HttpPost("update-pubished-lottery/{id}/{isPublished}")]
	public IActionResult UpdatePublishedLottery(int id, bool isPublished)
	{
		try
		{
			var lottery = _unitOfWork.LotteryRepository.GetById(id);
			if (lottery == null)
				return NotFound(new Response<LotteryVm>()
				{
					Errors = new[] { "Not found." }
				});
			lottery.IsPublished = isPublished;
			lottery.PublishDate = isPublished ? (lottery.DrawDate.AddDays(-1)) : DateTime.MinValue;
			_unitOfWork.LotteryRepository.Update(lottery);
			int result = _unitOfWork.SaveChanges();
			if (result <= 0)
			{
				return BadRequest(new Response<LotteryVm>()
				{
					Errors = new[] { "Lottery could not be update." }
				});
			}

			return Ok(new Response<LotteryVm>()
			{
				Data = new Data<LotteryVm>()
				{
					Result = [_mapper.Map<LotteryVm>(lottery)]
				}
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new Response<LotteryVm>()
			{
				Errors = new[] { ex.Message }
			});
		}
	}
}
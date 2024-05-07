using Asp.Versioning;
using AutoMapper;
using LotteryChecker.API.Helpers;
using LotteryChecker.API.Models;
using LotteryChecker.API.Models.Entities;
using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Infrastructures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LotteryChecker.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/lottery")]
public class LotteryController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public LotteryController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("get-all-lotteries")]
    public IActionResult GetAllLotteries([FromQuery] LotteryQuery query, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var lotteries = _unitOfWork.LotteryRepository.GetAll().ToList();
            if (!lotteries.IsNullOrEmpty())
            {
                lotteries = query.Filters
                    .Aggregate(lotteries, (current, filter) => current.Where(filter).ToList());
                var lotteryPaging = _unitOfWork.LotteryRepository
                    .GetPaging(lotteries, null, page, pageSize).ToList();

                var response = new HttpResponse<LotteryVm>()
                {
                    Result = lotteryPaging.Select(lottery => _mapper.Map<LotteryVm>(lottery)),
                    Meta = new Meta()
                    {
                        Page = page,
                        PageSize = pageSize > lotteryPaging.Count ? lotteryPaging.Count : pageSize,
                        TotalPages = (int)Math.Ceiling((decimal)lotteries.Count / pageSize)
                    }
                };
                
                return Ok(response);
            }
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        } 
    }
     
    [HttpGet("get-lottery-by-id")]
    public IActionResult GetLotteryById(int id)
    {
        try
        {
            var lottery = _unitOfWork.LotteryRepository.GetById(id);
            var lotteryVm = _mapper.Map<LotteryVm>(lottery);
            if (lottery == null)
                return NotFound();
            return Ok(lotteryVm);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("create-lottery")]
    public IActionResult CreateLottery([FromBody] LotteryVm lotteryVm)
    {
        try
        {
            var lottery = _mapper.Map<Lottery>(lotteryVm);
            _unitOfWork.LotteryRepository.Create(lottery);
            int result = _unitOfWork.SaveChanges();
            if (result > 0)
                return Ok(lottery);
            return BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("update-lottery")]
    public IActionResult UpdateLottery([FromBody] LotteryVm lotteryVm)
    {
        try
        {
            var lottery = _mapper.Map<Lottery>(lotteryVm);
            _unitOfWork.LotteryRepository.Update(lottery);
            int result = _unitOfWork.SaveChanges();
            if (result > 0)
                return Ok();
            return BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
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
                return Ok();
            return BadRequest();
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
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
                return Ok(lotteries);
            }

            return BadRequest("Cannot generate results!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
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
        return Ok(_mapper.Map<List<LotteryVm>>(lotteryResult));
    }
}
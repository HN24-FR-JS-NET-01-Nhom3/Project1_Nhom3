using Asp.Versioning;
using AutoMapper;
using LotteryChecker.API.Models.Entities;
using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Infrastructures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LotteryChecker.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:ApiVersion}/reward")]
public class RewardController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RewardController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("get-all-rewards")]
    public IActionResult GetAllRewards([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var rewards = _unitOfWork.RewardRepository.GetAll().ToList();

        if (!rewards.IsNullOrEmpty())
        {
            var rewardPaging = _unitOfWork.RewardRepository.GetPaging(rewards, null, page, pageSize).ToList();

            return Ok(new
            {
                Result = rewardPaging.Select(reward => _mapper.Map<RewardVm>(reward)),
                Meta = new
                {
                    page,
                    pageSize = pageSize > rewardPaging.Count ? rewardPaging.Count : pageSize,
                    totalPages = (int)Math.Ceiling((decimal)rewards.Count / pageSize)
                }
            });
        }

        return NotFound();
    }

    [HttpGet("get-reward/{id}")]
    public IActionResult GetReward(int id)
    {
        var reward = _unitOfWork.RewardRepository.GetById(id);
        if (reward == null)
        {
            return NotFound();
        }

        var rewardVm = _mapper.Map<RewardVm>(reward);
        return Ok(rewardVm);
    }

    [HttpPost("create-reward")]
    public ActionResult CreateReward(RewardVm? rewardVm)
    {
        if (rewardVm == null)
        {
            return NotFound();
        }

        var reward = _mapper.Map<Reward>(rewardVm);

        try
        {
            _unitOfWork.RewardRepository.Create(reward);
            var status = _unitOfWork.SaveChanges();
            if (status > 0)
            {
                return Ok();
            }
            return BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("update-reward/{id}")]
    public ActionResult UpdateReward(int id, RewardVm? rewardVm)
    {
        if (rewardVm == null)
        {
            return NotFound();
        }

        var reward = _mapper.Map<Reward>(rewardVm);
        reward.RewardId = id;

        try
        {
            _unitOfWork.RewardRepository.Update(reward);
            var status = _unitOfWork.SaveChanges();
            if (status > 0)
            {
                return Ok();
            }
            return BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("delete-reward/{id}")]
    public ActionResult DeleteReward(int id)
    {
        var reward = _unitOfWork.RewardRepository.GetById(id);
        if (reward == null)
        {
            return NotFound();
        }

        try
        {
            _unitOfWork.RewardRepository.Delete(reward);
            var status = _unitOfWork.SaveChanges();
            if (status > 0)
            {
                return Ok();
            }
            return BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
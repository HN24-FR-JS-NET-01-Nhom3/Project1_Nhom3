using Asp.Versioning;
using AutoMapper;
using LotteryChecker.Common.Models.Entities;
using LotteryChecker.Common.Models.Http;
using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Infrastructures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LotteryChecker.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/reward")]
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
        try
        {
            var rewards = _unitOfWork.RewardRepository.GetAll().OrderBy(r => r.RewardId).ToList();

            if (!rewards.IsNullOrEmpty())
            {
                var rewardPaging = _unitOfWork.RewardRepository.GetPaging(rewards, null, page, pageSize).ToList();
                
                var response = new Response<RewardVm>()
                {
                    Data = new Data<RewardVm>()
                    {
                        Result = _mapper.Map<IEnumerable<RewardVm>>(rewardPaging),
                        Meta = new Meta()
                        {
                            Page = page,
                            PageSize = pageSize > rewardPaging.Count ? rewardPaging.Count : pageSize,
                            TotalPages = (int)Math.Ceiling((decimal)rewards.Count / pageSize)
                        }
                    }
                };
                
                return Ok(response);
            }

            return NotFound(new Response<RewardVm>()
            {
                Errors = new[] { "No rewards found" }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response<RewardVm>()
            {
                Errors = new[] { ex.Message }
            });
        }
         
    }

    [HttpGet("get-reward/{id}")]
    public IActionResult GetRewardById(int id)
    {
        try
        {
            var reward = _unitOfWork.RewardRepository.GetById(id);
            if (reward == null)
            {
                return NotFound(new Response<RewardVm>()
                {
                    Errors = new[] { "No rewards found" }
                });
            }

            var rewardVm = _mapper.Map<RewardVm>(reward);
            return Ok(new Response<RewardVm>()
            {
                Data = new Data<RewardVm>()
                {
                    Result = [rewardVm]
                }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response<RewardVm>()
            {
                Errors = new[] { ex.Message }
            });
        }
    }

    [HttpPost("create-reward")]
    public IActionResult CreateReward([FromBody] RewardVm rewardVm)
    {
        try
        {
            var reward = _mapper.Map<Reward>(rewardVm);
            _unitOfWork.RewardRepository.Create(reward);
            var status = _unitOfWork.SaveChanges();

            if (status > 0)
            {
                return Ok(new Response<Reward>()
                {
                    Data = new Data<Reward>()
                    {
                        Result = [reward]
                    }
                });
            }
            return BadRequest(new Response<Reward>()
            {
                Errors = new[] { "Error happened when saving reward to database" }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response<Reward>()
            {
                Errors = new[] { ex.Message }
            });
        }
    }

    [HttpPut("update-reward/{id}")]
    public IActionResult UpdateReward(int id, RewardVm rewardVm)
    {
        try
        {
            var reward = _mapper.Map<Reward>(rewardVm);
            reward.RewardId = id;
            _unitOfWork.RewardRepository.Update(reward);
            var status = _unitOfWork.SaveChanges();
           
            if (status > 0)
            {
                return Ok(new Response<Reward>()
                {
                    Data = new Data<Reward>()
                    {
                        Result = [reward]
                    }
                });
            }
            return BadRequest(new Response<Reward>()
            {
                Errors = new[] { "Error happened when saving reward to database" }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response<Reward>()
            {
                Errors = new[] { ex.Message }
            });
        }
    }

    [HttpDelete("delete-reward/{id}")]
    public IActionResult DeleteReward(int id)
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
                return Ok(new Response<Reward>()
                {
                    Message = "Reward deleted successfully!"
                });
            }
            return BadRequest(new Response<Reward>()
            {
                Errors = new[] { "Error happened when deleting reward to database" }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response<Reward>()
            {
                Errors = new[] { ex.Message }
            });
        }
    }
}
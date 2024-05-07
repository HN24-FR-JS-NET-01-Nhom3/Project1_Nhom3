using Asp.Versioning;
using AutoMapper;
using LotteryChecker.API.Models.Entities;
using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Infrastructures;
using Microsoft.AspNetCore.Mvc;

namespace LotteryChecker.API.Controllers.v1
{
   
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{v:ApiVersion}/rewards")]
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
        public IActionResult GetAllRewards()
        {
            var rewards = _unitOfWork.RewardRepository.GetAll();
            var rewardsVm = _mapper.Map<IEnumerable<RewardVm>>(rewards);
            return Ok(rewardsVm);
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
}

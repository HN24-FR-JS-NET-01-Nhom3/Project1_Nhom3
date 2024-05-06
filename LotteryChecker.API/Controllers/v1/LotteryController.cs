using AutoMapper;
using LotteryChecker.API.Helpers;
using LotteryChecker.API.Models.Entities;
using LotteryChecker.Core.Data;
using LotteryChecker.Core.Entities;
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
        private IMapper _mapper;

        public LotteryController(LotteryContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("get-all-lotteries")]
        public IActionResult GetAllLotteries([FromQuery] LotteryQuery query, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var lotteries =  _unitOfWork.LotteryRepository.GetAll();     
            if(lotteries != null)
            {
                lotteries = query.Filters
                .Aggregate(lotteries, (current, filter) => current.Where(filter).ToList());
                var lotteryPaging = _unitOfWork.LotteryRepository
                    .GetPaging(lotteries, null, page, pageSize).ToList();

                return Ok(new
                {
                    Result = lotteryPaging.Select(lottery => _mapper.Map<LotteryVm>(lottery)),
                    Meta = new
                    {
                        page,
                        pageSize = pageSize > lotteryPaging.Count ? lotteryPaging.Count : pageSize,
                        totalPages = (int)Math.Ceiling((decimal)lotteries.Count() / pageSize)
                    }
                });
            }
            return NotFound();
        }
     
        [HttpGet("get-lottery-by-id")]
        public IActionResult GetLotteryById(int id)
        {
            var lottery = _unitOfWork.LotteryRepository.GetById(id);
            var lotteryVM = _mapper.Map<LotteryVm>(lottery);
            if(lottery == null)
                return NotFound();
            return Ok(lotteryVM);
        }

        [HttpPost("create-lottery")]
        public IActionResult CreateLottery([FromBody] LotteryVm lotteryVm)
        {

            var lottery = _mapper.Map<Lottery>(lotteryVm);
            _unitOfWork.LotteryRepository.Create(lottery);
            int result = _unitOfWork.SaveChanges();
            if(result > 0)
                return Ok();
            return BadRequest();
        }

        [HttpPut("update-lottery")]
        public IActionResult UpdateLottery([FromBody] LotteryVm lotteryVm)
        {
            var lottery = _mapper.Map<Lottery>(lotteryVm);
            _unitOfWork.LotteryRepository.Update(lottery);
            int result = _unitOfWork.SaveChanges();
            if(result > 0)
                return Ok();
            return BadRequest();
        }

        [HttpDelete("delete-lottery")]
        public IActionResult DeleteLottery(int id)
        {
            var lottery = _unitOfWork.LotteryRepository.GetById(id);
            if(lottery == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfWork.LotteryRepository.Delete(id);
                int result = _unitOfWork.SaveChanges();
                if (result > 0)
                    return Ok();
                return BadRequest();
            }    
        }
    }
}

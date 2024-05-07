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
    [Route("api/v{v:apiVersion}/search-history")]
    public class SearchHistoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchHistoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("get-all-search-histories")]
        public IActionResult GetAllSearchHistories([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var searchHistories = _unitOfWork.SearchHistoryRepository.GetAll();

                if (searchHistories != null)
                {
                    var searchHistoryPaging = _unitOfWork.SearchHistoryRepository.GetPaging(searchHistories, null, page, pageSize).ToList();

                    return Ok(new
                    {
                        Result = searchHistoryPaging.Select(searchHistory => _mapper.Map<SearchHistoryVm>(searchHistory)),
                        Meta = new
                        {
                            page,
                            pageSize = pageSize > searchHistoryPaging.Count ? searchHistoryPaging.Count : pageSize,
                            totalPages = (int)System.Math.Ceiling((decimal)searchHistories.Count() / pageSize)
                        }
                    });
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("get-search-history/{id}")]
        public IActionResult GetSearchHistory(int id)
        {
            try
            {
                var searchHistory = _unitOfWork.SearchHistoryRepository.GetById(id);
                if (searchHistory == null)
                {
                    return NotFound();
                }

                var searchHistoryVm = _mapper.Map<SearchHistoryVm>(searchHistory);
                return Ok(searchHistoryVm);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("create-search-history")]
        public ActionResult CreateSearchHistory(SearchHistoryVm? searchHistoryVm)
        {
            if (searchHistoryVm == null)
            {
                return BadRequest();
            }

            var searchHistory = _mapper.Map<SearchHistory>(searchHistoryVm);

            try
            {
                _unitOfWork.SearchHistoryRepository.Create(searchHistory);
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

        [HttpPut("update-search-history")]
        public IActionResult UpdateSearchHistory(int id, SearchHistoryVm? searchHistoryVm)
        {
            if (searchHistoryVm == null)
            {
                return BadRequest();
            }

            var searchHistory = _mapper.Map<SearchHistory>(searchHistoryVm);
            searchHistory.SearchHistoryId = id;

            try
            {
                _unitOfWork.SearchHistoryRepository.Update(searchHistory);
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

        [HttpDelete("delete-search-history/{id}")]
        public IActionResult DeleteSearchHistory(int id)
        {
            var searchHistory = _unitOfWork.SearchHistoryRepository.GetById(id);
            if (searchHistory == null)
            {
                return NotFound();
            }

            try
            {
                _unitOfWork.PurchaseTicketRepository.Delete(id);
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

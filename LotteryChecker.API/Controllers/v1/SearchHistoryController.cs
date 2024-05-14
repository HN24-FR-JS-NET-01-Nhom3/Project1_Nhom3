using Asp.Versioning;
using AutoMapper;
using LotteryChecker.Common.Models.Entities;
using LotteryChecker.Common.Models.Http;
using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace LotteryChecker.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/search-history")]
[Authorize(Roles = "User, Admin")]
public class SearchHistoryController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SearchHistoryController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet("get-search-histories-by-user-id")]
    public IActionResult GetSearchHistoriesByUserId()
    {
        try
        {
            var userId = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var searchHistories = _unitOfWork.SearchHistoryRepository.GetByUserId(userId).OrderByDescending(x => x.SearchDate).ToList().Take(5);
            if (!searchHistories.IsNullOrEmpty())
            {
                var response = new Response<SearchHistoryVm>()
                {
                    Data = new Data<SearchHistoryVm>()
                    { 
                        Result = _mapper.Map<IEnumerable<SearchHistoryVm>>(searchHistories)
                    }
                };
                return Ok(response);
            }
            return NotFound(new Response<SearchHistoryVm>()
            {
                Errors = new[] { "No search historys found" }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response<SearchHistoryVm>()
            {
                Errors = new[] { ex.Message }
            });
        }
    }

    [HttpGet("get-all-search-histories")]
    public IActionResult GetAllSearchHistories([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var searchHistories = _unitOfWork.SearchHistoryRepository.GetAll().ToList();
            if (!searchHistories.IsNullOrEmpty())
            {
                var searchHistoryPaging = _unitOfWork.SearchHistoryRepository.GetPaging(searchHistories, null, page, pageSize).ToList();
                
                var response = new Response<SearchHistoryVm>()
                {
                    Data = new Data<SearchHistoryVm>()
                    {
                        Result = _mapper.Map<IEnumerable<SearchHistoryVm>>(searchHistoryPaging),
                        Meta = new Meta()
                        {
                            Page = page,
                            PageSize = pageSize > searchHistoryPaging.Count ? searchHistoryPaging.Count : pageSize,
                            TotalPages = (int)Math.Ceiling((decimal)searchHistories.Count / pageSize)
                        }
                    }
                };
                
                return Ok(response);
            }

            return NotFound(new Response<SearchHistoryVm>()
            {
                Errors = new[] { "No search historys found" }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response<SearchHistoryVm>()
            {
                Errors = new[] { ex.Message }
            });
        }
         
    }

    [HttpGet("get-search-history/{id}")]
    public IActionResult GetSearchHistoryById(int id)
    {
        try
        {
            var searchHistory = _unitOfWork.SearchHistoryRepository.GetById(id);
            if (searchHistory == null)
            {
                return NotFound(new Response<SearchHistoryVm>()
                {
                    Errors = new[] { "No search historys found" }
                });
            }

            var searchHistoryVm = _mapper.Map<SearchHistoryVm>(searchHistory);
            return Ok(new Response<SearchHistoryVm>()
            {
                Data = new Data<SearchHistoryVm>()
                {
                    Result = [searchHistoryVm]
                }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response<SearchHistoryVm>()
            {
                Errors = new[] { ex.Message }
            });
        }
    }

    [HttpPost("create-search-history")]
    public IActionResult CreateSearchHistory([FromBody] SearchHistoryVm searchHistoryVm)
    {
        try
        {
            var searchHistory = _mapper.Map<SearchHistory>(searchHistoryVm);
            _unitOfWork.SearchHistoryRepository.Create(searchHistory);
            var status = _unitOfWork.SaveChanges();

            if (status > 0)
            {
                return Ok(new Response<SearchHistory>()
                {
                    Data = new Data<SearchHistory>()
                    {
                        Result = [searchHistory]
                    }
                });
            }
            return BadRequest(new Response<SearchHistory>()
            {
                Errors = new[] { "Error happened when saving search history to database" }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response<SearchHistory>()
            {
                Errors = new[] { ex.Message }
            });
        }
    }

    [HttpPut("update-search-history/{id}")]
    public IActionResult UpdateSearchHistory(int id, SearchHistoryVm searchHistoryVm)
    {
        try
        {
            var searchHistory = _mapper.Map<SearchHistory>(searchHistoryVm);
            searchHistory.SearchHistoryId = id;
            _unitOfWork.SearchHistoryRepository.Update(searchHistory);
            var status = _unitOfWork.SaveChanges();
           
            if (status > 0)
            {
                return Ok(new Response<SearchHistory>()
                {
                    Data = new Data<SearchHistory>()
                    {
                        Result = [searchHistory]
                    }
                });
            }
            return BadRequest(new Response<SearchHistory>()
            {
                Errors = new[] { "Error happened when saving search history to database" }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response<SearchHistory>()
            {
                Errors = new[] { ex.Message }
            });
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
            _unitOfWork.SearchHistoryRepository.Delete(searchHistory);
            var status = _unitOfWork.SaveChanges();

            if (status > 0)
            {
                return Ok(new Response<SearchHistory>()
                {
                    Message = "Search history deleted successfully!"
                });
            }
            return BadRequest(new Response<SearchHistory>()
            {
                Errors = new[] { "Error happened when deleting search history to database" }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response<SearchHistory>()
            {
                Errors = new[] { ex.Message }
            });
        }
    }
}
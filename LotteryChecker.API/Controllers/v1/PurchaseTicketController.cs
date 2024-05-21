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
[Route("api/v{v:apiVersion}/purchase-ticket")]
[Authorize(Roles = "User, Admin")]
public class PurchaseTicketController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PurchaseTicketController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("get-all-purchase-tickets")]
    public IActionResult GetAllPurchaseTickets([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
    {
        try
        {
            var purchaseTickets = _unitOfWork.PurchaseTicketRepository.GetAllPurchaseTickets().ToList();

            if (!purchaseTickets.IsNullOrEmpty())
            {
                var purchaseTicketPaging = _unitOfWork.PurchaseTicketRepository.GetPaging(purchaseTickets, null, page, pageSize).ToList();
                
                var response = new Response<PurchaseTicketVm>()
                {
                    Data = new Data<PurchaseTicketVm>()
                    {
                        Result = _mapper.Map<IEnumerable<PurchaseTicketVm>>(purchaseTicketPaging),
                        Meta = new Meta()
                        {
                            Page = page,
                            PageSize = pageSize > purchaseTicketPaging.Count ? purchaseTicketPaging.Count : pageSize,
                            TotalPages = (int)Math.Ceiling((decimal)purchaseTickets.Count / pageSize)
                        }
                    }
                };
                
                return Ok(response);
            }

            return NotFound(new Response<PurchaseTicketVm>()
            {
                Errors = new[] { "No purchase tickets found" }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response<PurchaseTicketVm>()
            {
                Errors = new[] { ex.Message }
            });
        }
    }

    [HttpGet("get-purchase-ticket/{id}")]
    public IActionResult GetPurchaseTicketById(int id)
    {
        try
        {
            var purchaseTicket = _unitOfWork.PurchaseTicketRepository.GetById(id);
            if (purchaseTicket == null)
            {
                return NotFound(new Response<PurchaseTicketVm>()
                {
                    Errors = new[] { "No purchase tickets found" }
                });
            }

            var purchaseTicketVm = _mapper.Map<PurchaseTicketVm>(purchaseTicket);
            return Ok(new Response<PurchaseTicketVm>()
            {
                Data = new Data<PurchaseTicketVm>()
                {
                    Result = [purchaseTicketVm]
                }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response<PurchaseTicketVm>()
            {
                Errors = new[] { ex.Message }
            });
        }
    }

    [HttpPost("create-purchase-ticket")]
    public IActionResult CreatePurchaseTicket([FromBody] PurchaseTicketVm purchaseTicketVm)
    {
        try
        {
            var purchaseTicket = _mapper.Map<PurchaseTicket>(purchaseTicketVm);
            _unitOfWork.PurchaseTicketRepository.Create(purchaseTicket);
            var status = _unitOfWork.SaveChanges();

            if (status > 0)
            {
                return Ok(new Response<PurchaseTicket>()
                {
                    Data = new Data<PurchaseTicket>()
                    {
                        Result = [purchaseTicket]
                    }
                });
            }
            return BadRequest(new Response<PurchaseTicket>()
            {
                Errors = new[] { "Error happened when saving purchase ticket to database" }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response<PurchaseTicket>()
            {
                Errors = new[] { "Error occurred while saving the entity changes", ex.InnerException?.Message }
            });
        }
    }

    [HttpPut("update-purchase-ticket/{id}")]
    public IActionResult UpdatePurchaseTicket(int id, PurchaseTicketVm purchaseTicketVm)
    {
        try
        {
            var purchaseTicket = _mapper.Map<PurchaseTicket>(purchaseTicketVm);
            purchaseTicket.PurchaseTicketId = id;
            _unitOfWork.PurchaseTicketRepository.Update(purchaseTicket);
            var status = _unitOfWork.SaveChanges();
           
            if (status > 0)
            {
                return Ok(new Response<PurchaseTicket>()
                {
                    Data = new Data<PurchaseTicket>()
                    {
                        Result = [purchaseTicket]
                    }
                });
            }
            return BadRequest(new Response<PurchaseTicket>()
            {
                Errors = new[] { "Error happened when saving purchase ticket to database" }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response<PurchaseTicket>()
            {
                Errors = new[] { ex.Message }
            });
        }
    }

    [HttpDelete("delete-purchase-ticket/{id}")]
    public IActionResult DeletePurchaseTicket(int id)
    {
        var purchaseTicket = _unitOfWork.PurchaseTicketRepository.GetById(id);
        if (purchaseTicket == null)
        {
            return NotFound();
        }

        try
        {
            _unitOfWork.PurchaseTicketRepository.Delete(purchaseTicket);
            var status = _unitOfWork.SaveChanges();

            if (status > 0)
            {
                return Ok(new Response<PurchaseTicket>()
                {
                    Message = "Purchase ticket deleted successfully!"
                });
            }
            return BadRequest(new Response<PurchaseTicket>()
            {
                Errors = new[] { "Error happened when deleting purchase ticket to database" }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response<PurchaseTicket>()
            {
                Errors = new[] { ex.Message }
            });
        }
    }
    
    [HttpGet("get-all-purchase-tickets-by-user")]
    public IActionResult GetAllPurchaseTicketsByUser([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
    {
        try
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var purchaseTickets = _unitOfWork.PurchaseTicketRepository.Find(pt => pt.UserId.ToString() == userId).ToList();

            if (!purchaseTickets.IsNullOrEmpty())
            {
                var purchaseTicketPaging = _unitOfWork.PurchaseTicketRepository.GetPaging(purchaseTickets, null, page, pageSize).ToList();
                
                var response = new Response<PurchaseTicketVm>()
                {
                    Data = new Data<PurchaseTicketVm>()
                    {
                        Result = _mapper.Map<IEnumerable<PurchaseTicketVm>>(purchaseTicketPaging),
                        Meta = new Meta()
                        {
                            Page = page,
                            PageSize = pageSize > purchaseTicketPaging.Count ? purchaseTicketPaging.Count : pageSize,
                            TotalPages = (int)Math.Ceiling((decimal)purchaseTickets.Count / pageSize)
                        }
                    }
                };
                
                return Ok(response);
            }

            return NotFound(new Response<PurchaseTicketVm>()
            {
                Errors = new[] { "No purchase tickets found" }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response<PurchaseTicketVm>()
            {
                Errors = new[] { ex.Message }
            });
        }
    }
}
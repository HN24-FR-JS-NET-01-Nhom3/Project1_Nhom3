using Asp.Versioning;
using AutoMapper;
using LotteryChecker.API.Models;
using LotteryChecker.API.Models.Entities;
using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Infrastructures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LotteryChecker.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/purchase-ticket")]
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
    public IActionResult GetAllPurchaseTickets([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var purchaseTickets = _unitOfWork.PurchaseTicketRepository.GetAllPurchaseTickets().ToList();

            if (!purchaseTickets.IsNullOrEmpty())
            {
                var purchaseTicketPaging = _unitOfWork.PurchaseTicketRepository.GetPaging(purchaseTickets, null, page, pageSize).ToList();
                
                var response = new HttpResponse<PurchaseTicketVm>()
                {
                    Result = purchaseTicketPaging.Select(purchaseTicket => _mapper.Map<PurchaseTicketVm>(purchaseTicket)),
                    Meta = new Meta()
                    {
                        Page = page,
                        PageSize = pageSize > purchaseTicketPaging.Count ? purchaseTicketPaging.Count : pageSize,
                        TotalPages = (int)Math.Ceiling((decimal)purchaseTickets.Count / pageSize)
                    }
                };
                
                return Ok(response);
            }

            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
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
                return NotFound();
            }

            var purchaseTicketVm = _mapper.Map<PurchaseTicketVm>(purchaseTicket);
            return Ok(purchaseTicketVm);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("create-purchase-ticket")]
    public IActionResult CreatePurchaseTicket(PurchaseTicketVm? purchaseTicketVm)
    {
        if (purchaseTicketVm == null)
        {
            return NotFound();
        }

        var purchaseTicket = _mapper.Map<PurchaseTicket>(purchaseTicketVm);

        try
        {
            _unitOfWork.PurchaseTicketRepository.Create(purchaseTicket);
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

    [HttpPut("update-purchase-ticket/{id}")]
    public IActionResult UpdatePurchaseTicket(int id, PurchaseTicketVm? purchaseTicketVm)
    {
        if (purchaseTicketVm == null)
        {
            return NotFound();
        }

        var purchaseTicket = _mapper.Map<PurchaseTicket>(purchaseTicketVm);
        purchaseTicket.PurchaseTicketId = id;

        try
        {
            _unitOfWork.PurchaseTicketRepository.Update(purchaseTicket);
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
﻿using AutoMapper;
using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Infrastructures;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Asp.Versioning;
using Microsoft.IdentityModel.Tokens;
using LotteryChecker.Common.Models.ViewModels;
using System.Net;
using LotteryChecker.Common.Models.Authentications;

namespace LotteryChecker.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/user")]
public class UserController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private UserManager<AppUser> _userManager;
    public UserController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }
    
    [HttpGet("get-all-users/page={page}&pageSize={pageSize}")]
    public IActionResult GetAllUsers(int page = 1, int pageSize = 5)
    {
        try
        {
            var users = _unitOfWork.UserRepository.GetAll().ToList();
            var userPagings = _unitOfWork.UserRepository.GetPaging(users, null, page, pageSize);
            if (userPagings.IsNullOrEmpty())
                return NotFound();
            var userPagingsVm = _mapper.Map<IEnumerable<UserVm>>(userPagings);
            return Ok(new UserPagingVm()
            {
                Result = userPagingsVm,
                Meta = new Meta()
                {
                    Page = page,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling((decimal)users.Count / pageSize)
                }
            });
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }       
    }

    [HttpGet("get-user/{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        try
        {
            string idStr = id.ToString();
            var user = await _userManager.FindByIdAsync(idStr);
            if (user == null)
                return NotFound();
            else
            {
                var userVm = _mapper.Map<UserVm>(user);
                return Ok(userVm);
            }
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserVm userVm)
    {
        try
        {
            var userExists = await _userManager.FindByEmailAsync(userVm.Email);
            var userNameExists = await _userManager.FindByNameAsync(userVm.UserName);
            if (userExists != null || userNameExists != null)
            {
                return BadRequest(new
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = $"User {userVm.Email} already exists.",
                });
            }

            var newUser = new AppUser()
            {
                UserName = userVm.UserName,
                Email = userVm.Email,
                FirstName = userVm.FirstName,
                LastName = userVm.LastName,
                PhoneNumber = userVm.Phone,
                PhoneNumberConfirmed = true,
                IsActive = true,
                SecurityStamp = new Guid().ToString()
            };

            var result = await _userManager.CreateAsync(newUser, userVm.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "User could not be created.",
                });
            }

            //return Created(nameof(CreateUser), $"User {userVm.Email} created.");
            return Ok(_mapper.Map<UserVm>(newUser));
        }
        catch(Exception ex)
        { 
            return BadRequest(new
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = ex.Message,
            });
        }
    }

    [HttpPut("update-user/{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserVm userVm)
    {
        try
        {
            string idStr = id.ToString();
            var user = await _userManager.FindByIdAsync(idStr);
            if (user == null)
            {
                return NotFound($"User not found.");
            }
            PropertyInfo[] properties = typeof(UserVm).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính trong userVm
                var value = property.GetValue(userVm);

                // Nếu giá trị không null và không phải chuỗi rỗng, cập nhật vào user
                if (value != null && !string.IsNullOrEmpty(value.ToString()))
                {
                    // Kiểm tra xem user có thuộc tính tương ứng không
                    var userProperty = user.GetType().GetProperty(property.Name);
                    if (userProperty != null && userProperty.CanWrite)
                    {
                        // Gán giá trị từ userVm vào user
                        userProperty.SetValue(user, value);
                    }
                }
            }
            await _userManager.UpdateAsync(user);
            return Ok(_mapper.Map<UserVm>(user));
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }
        
    [HttpPatch("update-block-user/{id}/{isActive}")]
    public async Task<IActionResult> UpdateBlockUser(Guid id, bool isActive)
    {
        try
        {
            string idStr = id.ToString();
            var user = await _userManager.FindByIdAsync(idStr);
            if (user == null)
                return NotFound();
            user.IsActive = isActive;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest("User could not be update.");
            }
            return Ok(_mapper.Map<UserVm>(user));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
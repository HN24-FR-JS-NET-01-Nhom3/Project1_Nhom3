using AutoMapper;
using LotteryChecker.API.Models.Entities;
using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Infrastructures;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Asp.Versioning;
using Microsoft.IdentityModel.Tokens;

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
    [HttpGet("get-all-users")]
    public IActionResult GetAllUsers()
    {
        try
        {
            var users = _unitOfWork.UserRepository.GetAll().ToList();
            if (users.IsNullOrEmpty())
                return NotFound();
            var usersVm = _mapper.Map<IEnumerable<UserVm>>(users);
            return Ok(usersVm);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }       
    }

    [HttpGet("get-user/{email}")]
    public async Task<IActionResult> GetUser(string email)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);
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
            if (userExists != null)
            {
                return BadRequest($"User {userVm.Email} already exists.");
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
                return BadRequest("User could not be create.");
            }

            return Created(nameof(CreateUser), $"User {userVm.Email} created.");
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("update-user/{email}")]
    public async Task<IActionResult> UpdateUser(string email, [FromBody] CreateUserVm userVm)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"User {userVm.Email} not found.");
            }
            if (!string.IsNullOrEmpty(userVm.Email) && userVm.Email != email)
            {
                var existingUser = await _userManager.FindByEmailAsync(userVm.Email);
                if (existingUser != null)
                {
                    return BadRequest($"Email {userVm.Email} is already in use.");
                }
            }
            PropertyInfo[] properties = typeof(CreateUserVm).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
            return Ok($"User {userVm.Email} updated.");
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }
        
    [HttpPatch("update-block-user/{email}/{isActive}")]
    public async Task<IActionResult> UpdateBlockUser(string email, bool isActive)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound();
            user.IsActive = isActive;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest("User could not be update.");
            }
            return Ok($"User {user.Email} updated.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
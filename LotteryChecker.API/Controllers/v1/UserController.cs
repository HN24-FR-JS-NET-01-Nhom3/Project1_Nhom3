using AutoMapper;
using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Infrastructures;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Asp.Versioning;
using Microsoft.IdentityModel.Tokens;
using LotteryChecker.Common.Models.ViewModels;
using LotteryChecker.Common.Models.Http;
using Microsoft.AspNetCore.Authorization;
using OfficeOpenXml;

namespace LotteryChecker.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/user")]
public class UserController : ControllerBase
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	private UserManager<AppUser> _userManager;
	private readonly RoleManager<IdentityRole<Guid>> _roleManager;

	public UserController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager,
		RoleManager<IdentityRole<Guid>> roleManager)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_userManager = userManager;
		_roleManager = roleManager;
	}

	[Authorize(Roles = "Admin")]
	[HttpGet("get-all-users/page={page}&pageSize={pageSize}")]
	public IActionResult GetAllUsers(int page = 1, int pageSize = 5)
	{
		try
		{
			var users = _unitOfWork.UserRepository.GetAll().ToList();
			var userPagings = _unitOfWork.UserRepository.GetPaging(users, null, page, pageSize);
			if (userPagings.IsNullOrEmpty())
				return NotFound();

			var userPagingsVm = _mapper.Map<IEnumerable<UserVm>>(userPagings).ToList();

			// Cập nhật trường Role của từng phần tử trong danh sách
			foreach (var userVm in userPagingsVm)
			{
				userVm.Role = String.Join(",", _userManager.GetRolesAsync(_mapper.Map<AppUser>(userVm)).Result);
			}

			return Ok(new Response<UserVm>()
			{
				Data = new Data<UserVm>()
				{
					Result = userPagingsVm,
					Meta = new Meta()
					{
						Page = page,
						PageSize = pageSize,
						TotalPages = (int)Math.Ceiling((decimal)users.Count / pageSize)
					}
				}
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new Response<UserVm>()
			{
				Errors = new[] { ex.Message }
			});
		}
	}


	[Authorize(Roles = "Admin")]
	[HttpGet("get-user/{id}")]
	public async Task<IActionResult> GetUser(Guid id)
	{
		try
		{
			string idStr = id.ToString();
			var user = await _userManager.FindByIdAsync(idStr);
			if (user == null)
				return NotFound(new Response<UserVm>()
				{
					Errors = new[] { "Not found!" }
				});

			var userResult = _mapper.Map<UserVm>(user);
			var roles = await _userManager.GetRolesAsync(user);
			userResult.Role = String.Join(",", roles);
			
			return Ok(new Response<UserVm>()
			{
				Data = new Data<UserVm>()
				{
					Result = [userResult]
				}
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new Response<UserVm>()
			{
				Errors = new[] { ex.Message }
			});
		}
	}

	[Authorize(Roles = "Admin")]
	[HttpPost("create-user")]
	public async Task<IActionResult> CreateUser([FromBody] CreateUserVm userVm)
	{
		try
		{
			var userExists = await _userManager.FindByEmailAsync(userVm.Email);
			var userNameExists = await _userManager.FindByNameAsync(userVm.UserName);
			if (userExists != null || userNameExists != null)
			{
				return BadRequest(new Response<UserVm>()
				{
					Errors = new[] { $"User {userVm.Email} already exists." }
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
				return BadRequest(new Response<UserVm>()
				{
					Errors = new[] { "User could not be created." }
				});
			}

			if (userVm.Role == null)
			{
				await _userManager.AddToRoleAsync(newUser, "User");
			}
			else
			{
				await _userManager.AddToRoleAsync(newUser, userVm.Role);
			}

			var userResult = _mapper.Map<UserVm>(newUser);
			userResult.Role = String.Join(",", _userManager.GetRolesAsync(_mapper.Map<AppUser>(userResult)).Result);

			return Ok(new Response<UserVm>()
			{
				Data = new Data<UserVm>()
				{
					Result = [userResult]
				}
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new Response<UserVm>()
			{
				Errors = new[] { ex.Message }
			});
		}
	}

	[Authorize(Roles = "Admin")]
	[HttpPut("update-user/{id}")]
	public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserVm userVm)
	{
		try
		{
			string idStr = id.ToString();
			var user = await _userManager.FindByIdAsync(idStr);
			if (user == null)
			{
				return NotFound(new Response<UserVm>()
				{
					Errors = new[] { "User not found." }
				});
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


			await _userManager.RemoveFromRolesAsync(user, _userManager.GetRolesAsync(user).Result);
			await _userManager.AddToRoleAsync(user, userVm.Role);
			
			var userResult = _mapper.Map<UserVm>(user);
			userResult.Role = String.Join(",", _userManager.GetRolesAsync(_mapper.Map<AppUser>(userResult)).Result);

			return Ok(new Response<UserVm>()
			{
				Data = new Data<UserVm>()
				{
					Result = [_mapper.Map<UserVm>(user)]
				}
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new Response<UserVm>()
			{
				Errors = new[] { ex.Message }
			});
		}
	}

	[Authorize(Roles = "Admin")]
	[HttpPost("update-status-user/{id}/{isActive}")]
	public async Task<IActionResult> UpdateStatusUser(string id, bool isActive)
	{
		try
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
				return NotFound(new Response<UserVm>()
				{
					Errors = new[] { "Not found." }
				});
			user.IsActive = isActive;
			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded)
			{
				return BadRequest(new Response<UserVm>()
				{
					Errors = new[] { "User could not be update." }
				});
			}

			return Ok(new Response<UserVm>()
			{
				Data = new Data<UserVm>()
				{
					Result = [_mapper.Map<UserVm>(user)]
				}
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new Response<UserVm>()
			{
				Errors = new[] { ex.Message }
			});
		}
	}

	[Authorize(Roles = "Admin")]
	[HttpGet("get-all-roles")]
	public IActionResult GetAllRoles()
	{
		return Ok(new Response<IdentityRole<Guid>>()
		{
			Data = new Data<IdentityRole<Guid>>()
			{
				Result = _roleManager.Roles
			}
		});
	}
	
	[HttpPost("get-user-by-email")]
	public async Task<IActionResult> GetUser([FromBody] string email)
	{
		try
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
				return NotFound(new Response<UserVm>()
				{
					Errors = new[] { "Not found!" }
				});

			var userResult = _mapper.Map<UserVm>(user);
			var roles = await _userManager.GetRolesAsync(user);
			userResult.Role = String.Join(",", roles);
			
			return Ok(new Response<UserVm>()
			{
				Data = new Data<UserVm>()
				{
					Result = [userResult]
				}
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new Response<UserVm>()
			{
				Errors = new[] { ex.Message }
			});
		}
	}
	
	[HttpGet("excel-export")]
	public async Task<IActionResult> ExcelExport()
	{
		ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
		try
		{
			var users = _unitOfWork.UserRepository.GetAll().ToList();
			
			var userVms = _mapper.Map<IEnumerable<UserVm>>(users);

			foreach (var userVm in userVms)
			{
				userVm.Role = String.Join(",", _userManager.GetRolesAsync(_mapper.Map<AppUser>(userVm)).Result);
			}

			using (var package = new ExcelPackage())
			{
				var worksheet = package.Workbook.Worksheets.Add("Sheet1");
				worksheet.Cells.LoadFromCollection(userVms, true);

				// Return Excel file
				var stream = new MemoryStream(package.GetAsByteArray());
				return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "filename.xlsx");
			}
		}
		catch (Exception ex)
		{
			return BadRequest(new Response<UserVm>()
			{
				Errors = new[] { ex.Message }
			});
		}
	}
}
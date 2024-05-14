using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Asp.Versioning;
using AutoMapper;
using LotteryChecker.Common.Models.Authentications;
using LotteryChecker.Common.Models.Http;
using LotteryChecker.Common.Models.ViewModels;
using LotteryChecker.Core.Data;
using LotteryChecker.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LotteryChecker.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/authen")]
public class AuthenticationController : ControllerBase
{
	private readonly UserManager<AppUser> _userManager;
	private readonly RoleManager<IdentityRole<Guid>> _roleManager;
	private readonly SignInManager<AppUser> _signInManager;
	private readonly LotteryContext _context;
	private readonly IConfiguration _configuration;
	private readonly IMapper _mapper;
	private readonly TokenValidationParameters _tokenValidationParameters;

	public AuthenticationController(UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager,
		SignInManager<AppUser> signInManager, LotteryContext context, IConfiguration configuration, IMapper mapper,
		TokenValidationParameters tokenValidationParameters)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_signInManager = signInManager;
		_context = context;
		_configuration = configuration;
		_mapper = mapper;
		_tokenValidationParameters = tokenValidationParameters;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterVm registerVm)
	{
		var userExists = await _userManager.FindByEmailAsync(registerVm.Email);
		if (userExists != null)
		{
			return BadRequest(new Response<object>()
			{
				Errors = new[] { $"User {registerVm.Email} already exists!" }
			});
		}

		var newUser = new AppUser()
		{
			UserName = registerVm.UserName,
			Email = registerVm.Email,
			SecurityStamp = new Guid().ToString()
		};
		var result = await _userManager.CreateAsync(newUser, registerVm.Password);
		if (!result.Succeeded)
		{
			return BadRequest(new Response<object>()
			{
				Errors = new[] { "User could not be create!" }
			});
		}

		return Created(nameof(Register), new Response<object>()
		{
			Message = $"User {registerVm.Email} created!"
		});
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginVm payload)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(new Response<object>()
			{
				Errors = new[] { "Please, provide all required fields" }
			});
		}

		var user = await _userManager.FindByEmailAsync(payload.Email);
		if (user != null && await _userManager.CheckPasswordAsync(user, payload.Password))
		{
			var roles = await _userManager.GetRolesAsync(user);
			var tokenValue = await GenerateJwtToken(user, roles);
			user.LastLogin = DateTime.Now;
			user.IsActive = true;
			await _userManager.UpdateAsync(user);
			return Ok(new Response<AuthResultVm>()
			{
				Data = new Data<AuthResultVm>()
				{
					Result = [tokenValue]
				}
			});
		}

		return BadRequest(new Response<Lottery>()
		{
			Errors = new[] { "Incorrect username or password!" }
		});
	}

	[HttpPost("logout")]
	public async Task<IActionResult> Logout()
	{
		await _signInManager.SignOutAsync();
		return NoContent();
	}

	private async Task<AuthResultVm> GenerateJwtToken(AppUser user, IList<string> roles)
	{
		var authClaims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, user.UserName),
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(JwtRegisteredClaimNames.Email, user.Email),
			new Claim(JwtRegisteredClaimNames.Sub, user.Email),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		foreach (var role in roles)
		{
			authClaims.Add(new Claim(ClaimTypes.Role, role));
		}

		var authSigningKey =
			new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"] ?? string.Empty));
		var token = new JwtSecurityToken(
			issuer: _configuration["JWT:Issuer"],
			audience: _configuration["JWT:Audience"],
			expires: DateTime.UtcNow.AddMinutes(30),
			claims: authClaims,
			signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
		);
		var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

		var refreshToken = new RefreshToken
		{
			JwtId = token.Id,
			IsRevoked = false,
			UserId = user.Id,
			AddDate = DateTime.UtcNow,
			ExpireDate = DateTime.UtcNow.AddDays(1),
			Token = Guid.NewGuid() + "-" + Guid.NewGuid()
		};

		await _context.RefreshTokens.AddAsync(refreshToken);
		await _context.SaveChangesAsync();
		var response = new AuthResultVm
		{
			AccessToken = jwtToken,
			RefreshToken = refreshToken.Token,
			ExpiresAt = token.ValidTo,
			User = _mapper.Map<UserVm>(user)
		};
		return response;
	}

	[HttpGet("get-role")]
	public async Task<IActionResult> GetRole()
	{
		var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		if (userId != null)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				var roles = await _userManager.GetRolesAsync(user);
				return Ok(roles);
			}
		}

		return NotFound();
	}

	[HttpPost("refresh-token")]
	public async Task<IActionResult> RefreshToken([FromBody] TokenRequestVm tokenRequest)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(new Response<string>()
			{
				Errors = new[] { "Invalid data." }
			});
		}

		var authResult = await VerifyAndGenerateToken(tokenRequest);
		if (authResult.Data == null)
		{
			return BadRequest(new Response<string>()
			{
				Errors = new[] { "Invalid token." }
			});
		}

		return Ok(authResult);
	}

	private async Task<Response<AuthResultVm>> VerifyAndGenerateToken(TokenRequestVm tokenRequest)
	{
		var jwtTokenHandler = new JwtSecurityTokenHandler();

		try
		{
			var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.AccessToken,
				_tokenValidationParameters, out var validatedToken);

			if (validatedToken is JwtSecurityToken jwtSecurityToken)
			{
				var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
					StringComparison.InvariantCultureIgnoreCase);
				if (!result)
				{
					return null;
				}
			}

			var utcExpiryDate = long.Parse(tokenInVerification.Claims
				.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

			var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

			if (expiryDate > DateTime.UtcNow)
			{
				return new Response<AuthResultVm>()
				{
					Errors = new List<string> { "Access token has not expired yet." }
				};
			}

			var storedToken =
				await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);

			if (storedToken == null || storedToken.IsRevoked)
			{
				return new Response<AuthResultVm>()
				{
					Errors = new List<string> { "Invalid refresh token." }
				};
			}

			if (storedToken.ExpireDate < DateTime.UtcNow)
			{
				return new Response<AuthResultVm>()
				{
					Errors = new List<string> { "Refresh token has expired." }
				};
			}

			var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

			if (storedToken.JwtId != jti)
			{
				return new Response<AuthResultVm>()
				{
					Errors = new List<string> { "Token doesn't match." }
				};
			}

			storedToken.IsRevoked = true;
			_context.RefreshTokens.Update(storedToken);
			await _context.SaveChangesAsync();

			var dbUser = await _userManager.FindByIdAsync(storedToken.UserId.ToString());
			var roles = await _userManager.GetRolesAsync(dbUser);

			var tokenResponse = await GenerateJwtToken(dbUser, roles);

			return new Response<AuthResultVm>()
			{
				Data = new Data<AuthResultVm>()
				{
					Result = [tokenResponse]
				}
			};
		}
		catch (Exception)
		{
			return new Response<AuthResultVm>()
			{
				Errors = new List<string> { "Something went wrong." }
			};
		}
	}

	private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
	{
		var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();
		return dateTimeVal;
	}
}
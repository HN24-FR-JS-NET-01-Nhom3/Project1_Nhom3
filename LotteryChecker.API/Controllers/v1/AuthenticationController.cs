using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Asp.Versioning;
using LotteryChecker.API.Models;
using LotteryChecker.API.Models.Authentication;
using LotteryChecker.Core.Data;
using LotteryChecker.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LotteryChecker.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/authen")]
public class AuthenticationController : ControllerBase
{
	private readonly UserManager<AppUser> _userManager;
	private readonly RoleManager<IdentityRole<Guid>> _roleManager;
	private readonly LotteryContext _context;
	private readonly IConfiguration _configuration;

	public AuthenticationController(UserManager<AppUser> userManager,
		RoleManager<IdentityRole<Guid>> roleManager,
		LotteryContext context,
		IConfiguration configuration)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_context = context;
		_configuration = configuration;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterVm registerVm)
	{
		var userExists = await _userManager.FindByEmailAsync(registerVm.Email);
		if (userExists != null)
		{
			return BadRequest($"User {registerVm.Email} already exists!");
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
			return BadRequest("User could not be create!");
		}

		return Created(nameof(Register), $"User {registerVm.Email} created!");
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginVm payload)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest("Please, provide all required fields");
		}

		var user = await _userManager.FindByEmailAsync(payload.Email);
		if (user != null && await _userManager.CheckPasswordAsync(user, payload.Password))
		{
			var roles = await _userManager.GetRolesAsync(user);
			var tokenValue = await GenerateJwtToken(user, roles);
			return Ok(tokenValue);
		}

		return Unauthorized();
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
			Jwtld = token.Id,
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
			Token = jwtToken,
			RefreshToken = refreshToken.Token,
			ExpiresAt = token.ValidTo
		};
		return response;
	}
}
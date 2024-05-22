using LotteryChecker.Common.Models.Authentications;
using LotteryChecker.Common.Models.Http;
using LotteryChecker.MVC.Models;

namespace LotteryChecker.MVC.Utils;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

public class TokenMiddleware
{
	private readonly RequestDelegate _next;
	private readonly IConfiguration _configuration;

	public TokenMiddleware(RequestDelegate next, IConfiguration configuration)
	{
		_next = next;
		_configuration = configuration;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		var accessToken = context.Request.Cookies["AccessToken"];
		var refreshToken = context.Request.Cookies["RefreshToken"];

		if (!string.IsNullOrEmpty(accessToken))
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var jwtToken = tokenHandler.ReadToken(accessToken) as JwtSecurityToken;

			if (jwtToken != null && jwtToken.ValidTo < DateTime.Now)
			{
				var newToken = await RefreshTokenAsync(accessToken, refreshToken);

				if (!string.IsNullOrEmpty(newToken))
				{
					context.Response.Cookies.Append("AccessToken", newToken);
				}
			}
		}

		await _next(context);
	}

	private async Task<string> RefreshTokenAsync(string accessToken, string refreshToken)
	{
		var refreshTokenRequest = new TokenRequestVm() { AccessToken = accessToken, RefreshToken = refreshToken };

		var refreshTokenResponse = await HttpUtils<AuthResultVm>.SendRequest(
			HttpMethod.Post,
			$"{Constants.API_AUTHEN}/refresh-token",
			refreshTokenRequest
		);

		return refreshTokenResponse.Data?.Result?.First().AccessToken;
	}
}

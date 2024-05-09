using System.Net;
using LotteryChecker.API.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;

namespace LotteryChecker.API.Exceptions;

public static class ExtensionMiddlewareExtension
{
	public static void ConfigureBuildInExceptionHandler(this IApplicationBuilder builder)
	{
		builder.UseExceptionHandler(builderError =>
		{
			builderError.Run(async context =>
			{
				context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				context.Response.ContentType = "application/json";

				var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
				var contextRequest = context.Features.Get<IHttpRequestFeature>();

				if (contextFeature != null)
				{
					await context.Response.WriteAsync(new ErrorVm()
					{
						StatusCode = context.Response.StatusCode,
						Message = contextFeature.Error.Message,
						Path = contextRequest.Path
					}.ToString());
				}
			});
		});
	}
}
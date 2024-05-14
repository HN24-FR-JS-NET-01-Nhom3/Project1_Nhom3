using LotteryChecker.Core.Data;
using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("connectionString") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<LotteryContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(LotteryChecker.Common.AutoMapper.MyAutoMapper).Assembly);

builder.Services.AddAuthentication(options =>
	{
		options.DefaultScheme = "Cookies";
		options.DefaultChallengeScheme = "Cookies";
	})
	.AddCookie("Cookies", options =>
	{
		options.AccessDeniedPath = "/home/error";
		options.LoginPath = "/authen/login";
	});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.UseAuthentication();

app.UseMiddleware<TokenMiddleware>();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name : "areas",
    pattern : "{area:exists}/{controller}/{action}");

app.Run();

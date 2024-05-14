using LotteryChecker.Core.Data;
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
builder.Services.AddAuthentication().AddFacebook(options =>
{
	options.ClientId = "458255099920081";
	options.ClientSecret = "61a21b7c045a182ecbd89953755ba581";
});
builder.Services.AddRazorPages();

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
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name : "areas",
    pattern : "{area:exists}/{controller}/{action}");

app.Run();

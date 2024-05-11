using LotteryChecker.Core.Data;
using LotteryChecker.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("connectionString") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<LotteryContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(
		options =>
		{
			options.SignIn.RequireConfirmedAccount = false;
			options.SignIn.RequireConfirmedEmail = false;
		}).AddRoles<IdentityRole<Guid>>()
	.AddEntityFrameworkStores<LotteryContext>().AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(LotteryChecker.Common.AutoMapper.MyAutoMapper).Assembly);

builder.Services.ConfigureApplicationCookie(options =>
{
	//Location for your Custom Login Page
	options.LoginPath = "/authen/login";
});

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

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name : "areas",
    pattern : "{area:exists}/{controller}/{action}");

app.MapControllerRoute(
	name: "admin",
	pattern: "/admin/{controller=HomeAdmin}/{action=Index}/{id?}",
	defaults: new { area = "Admin" }
).RequireAuthorization();

app.MapRazorPages();

app.Run();

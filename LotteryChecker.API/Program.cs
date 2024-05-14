using System.Text;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using LotteryChecker.Core.Data;
using LotteryChecker.Core.Infrastructures;
using LotteryChecker.Core.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

	foreach (var description in provider.ApiVersionDescriptions)
	{
		c.SwaggerDoc(description.GroupName,
			new OpenApiInfo { Title = "Lottery Checker API", Version = description.ApiVersion.ToString() });
	}
	
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement()
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				},
				Scheme = "oauth2",
				Name = "Bearer",
				In = ParameterLocation.Header
			},
			new List<string>()
		}
	});
});

var connectionString = builder.Configuration.GetConnectionString("connectionString");
builder.Services.AddDbContext<LotteryContext>(options => { options.UseSqlServer(connectionString); });

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(LotteryChecker.Common.AutoMapper.MyAutoMapper).Assembly);

builder.Services.AddApiVersioning(x =>
	{
		x.DefaultApiVersion = new ApiVersion(1, 0);
		x.AssumeDefaultVersionWhenUnspecified = true;
		x.ReportApiVersions = true;
	})
	.AddApiExplorer(options =>
	{
		options.GroupNameFormat = "'v'VVV";
		options.SubstituteApiVersionInUrl = true;
	});


builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(
		options =>
		{
			options.SignIn.RequireConfirmedAccount = false;
			options.SignIn.RequireConfirmedEmail = false;
		})
	.AddEntityFrameworkStores<LotteryContext>().AddDefaultTokenProviders();

builder.Services
	.AddAuthentication(config =>
	{
		config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(options =>
	{
		options.SaveToken = true;
		options.RequireHttpsMetadata = false;
		options.TokenValidationParameters = new TokenValidationParameters()
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding
				.ASCII
				.GetBytes(builder.Configuration["JWT:Secret"] ?? string.Empty)),
			ValidateIssuer = true,
			ValidIssuer = builder.Configuration["JWT:Issuer"],
			ValidateAudience = true,
			ValidAudience = builder.Configuration["JWT:Audience"]
		};
	});

builder.Services.AddControllersWithViews()
	.AddNewtonsoftJson(options =>
		options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
	);

builder.Services.AddSingleton(new TokenValidationParameters
{
	ValidateIssuer = true,
	ValidateAudience = true,
	ValidateLifetime = false, // Here we are saying that we don't care about the token's expiration date
	ValidateIssuerSigningKey = true,
	ValidIssuer = builder.Configuration["JWT:Issuer"],
	ValidAudience = builder.Configuration["JWT:Audience"],
	IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
		foreach (var description in provider.ApiVersionDescriptions)
		{
			options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
				description.GroupName.ToUpperInvariant());
		}
	});
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
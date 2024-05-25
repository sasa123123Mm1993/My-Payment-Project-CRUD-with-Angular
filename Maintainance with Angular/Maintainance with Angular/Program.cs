using System.Text;
using Maintainance_with_Angular.Models;
using Maintainance_with_Angular.Models.Identity;
using Maintainance_with_Angular.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PaymentDetailContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddIdentityCore<AppUser>(opt =>
		{
			opt.Password.RequireNonAlphanumeric = false;
			opt.Password.RequireDigit = false;
			opt.Password.RequireLowercase = false;
			opt.Password.RequireUppercase = false;
			opt.User.AllowedUserNameCharacters = "œÃÕŒÂ⁄€›ﬁÀ’÷ÿﬂ„‰ «·»Ì”‘Ÿ“Ê…Ï·«—ƒ¡∆–abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ "; 
		})
	.AddRoles<AppRole>()
	.AddRoleManager<RoleManager<AppRole>>()
	.AddSignInManager<SignInManager<AppUser>>()
	.AddEntityFrameworkStores<PaymentDetailContext>()
	.AddDefaultTokenProviders();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
								.AddJwtBearer(options =>
								{
									options.TokenValidationParameters = new TokenValidationParameters
									{
										ValidateIssuerSigningKey = true,
										IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
										ValidateIssuer = false,
										ValidateAudience = false
									};
								});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
options.WithOrigins("http://localhost:4200")
.AllowAnyMethod()
.AllowAnyHeader());


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();

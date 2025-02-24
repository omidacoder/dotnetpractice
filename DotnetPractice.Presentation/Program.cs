using AutoMapper;
using DotnetPractice.Common.Utils;
using DotnetPractice.DataAccess;
using DotnetPractice.DataAccess.Models;
using DotnetPractice.DataAccess.Seeds;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
// Adding Services
builder.Services.AddDbContext<PostgresDbContext>(PostgresDbContext.Connection);
builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultPhoneProvider;
}).AddEntityFrameworkStores<PostgresDbContext>().AddDefaultTokenProviders().AddErrorDescriber<PersianIdentityErrorDescriber>();

var tokenSettings = builder.Configuration.GetSection("TokenSettings").Get<TokenSettings>();
builder.Services.AddSingleton<TokenSettings>(tokenSettings);
//builder.Services.AddTransient<Seeder>();
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSettings.Secret)),
            ValidateIssuer = true,
            ValidIssuer = tokenSettings.Site,
            ValidateAudience = true,
            ValidAudience = tokenSettings.Audience,
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("RequireNormalUserRole", policy => policy.RequireRole("NormalUser"));
    option.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    option.AddPolicy("Access", policy => policy.RequireRole("NormalUser", "Admin"));
});
builder.Services.AddSingleton(provider => new MapperConfiguration(options =>
{
    options.AddProfile(new AutoMapperProfile());
}).CreateMapper());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

}
app.UseCors(builder => {
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
});
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions()
{
    RequestPath = new PathString("/wwwroot")
});


app.MapControllers();

app.Run();

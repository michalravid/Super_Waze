using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MODELS.Models;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProjectContext>(op => op.UseSqlServer("Data Source=RUTI\\SQLEXPRESS;Initial Catalog=Super-Waze;Integrated Security=SSPI;Trusted_Connection=True;"));
builder.Services.AddScoped<IShop, ShopData>();
builder.Services.AddScoped<ICustomer, CustomerData>();
builder.Services.AddScoped<IProduct, ProductData>();

// Add JWT Authentication
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Add JWT service
builder.Services.AddScoped<JWT>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MODELS.Models;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BL;
using SuperWaze.Middleware;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// קביעת הגדרות Serilog
Log.Logger = new LoggerConfiguration()
    //.WriteTo.Console() // כתיבה לקונסולה
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) // כתיבה לקובץ עם תחלופה יומית
    .CreateLogger();

// הגדרת Serilog כ-Logging Provider
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // הוסף את התמיכה ב-JWT ל-Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<ProjectContext>(op => op.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Super-Waze;Integrated Security=SSPI;Trusted_Connection=True;"));
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Super_Waze API v1");
    });
}

app.UseHttpsRedirection();

// Enable authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Add middlewares
app.UseMiddleware<JwtMiddleware>();
app.UseMiddleware<LogMiddleware>();

app.MapControllers();

app.Run();

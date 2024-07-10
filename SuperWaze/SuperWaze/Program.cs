using DAL.Data;
using DAL.Interfaces;
using MODELS.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProjectContext>(op => op.UseSqlServer("Data Source='your DB name';Initial Catalog=Super-Waze;Integrated Security=SSPI;Trusted_Connection=True;"));
builder.Services.AddScoped<IShop, ShopData>();
builder.Services.AddScoped<ICustomer, CustomerData>();
builder.Services.AddScoped<IProduct, ProductData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

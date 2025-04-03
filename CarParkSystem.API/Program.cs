using AutoMapper.Extensions.ExpressionMapping;
using CarParkSystem.App.Interfaces;
using CarParkSystem.App.Mapping;
using CarParkSystem.App.Services;
using CarParkSystem.Data;
using CarParkSystem.Data.Storages;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Добавляем контекст базы данных
builder.Services.AddDbContext<CarParkSystemDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserStorage, UserStorage>();
builder.Services.AddScoped<IBidService, BidService>();
builder.Services.AddScoped<ISubdivisionService, SubdivisionService>();
builder.Services.AddScoped<IBidStorage, BidStorage>();
builder.Services.AddScoped<ISubdivisionStorage, SubdivisionStorage>();


// Добавляем AutoMapper — вариант с точной сборкой
builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
builder.Services.AddAutoMapper(typeof(EntityProfile).Assembly);
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddExpressionMapping();

}, AppDomain.CurrentDomain.GetAssemblies());

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

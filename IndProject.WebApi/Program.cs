using IndProject.WebApi.Controllers;
using IndProject.WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var sqlConnectionString = builder.Configuration.GetValue<string>("SqlConnectionString");
var sqlConnectionStringFound = !string.IsNullOrWhiteSpace(sqlConnectionString);
builder.Services.AddTransient<WeatherForecastRepository, WeatherForecastRepository>(o => new WeatherForecastRepository(sqlConnectionString));
var app = builder.Build();

app.MapGet("/", () => $"The API is up . Connection string found: {(sqlConnectionStringFound ? "Found" : "Not Found")}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

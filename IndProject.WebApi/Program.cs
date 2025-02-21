using IndProject.WebApi.Controllers;
using IndProject.WebApi.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var sqlConnectionString = builder.Configuration.GetValue<string>("SqlConnectionString");
var sqlConnectionStringFound = !string.IsNullOrWhiteSpace(sqlConnectionString);
builder.Services.AddTransient<WeatherForecastRepository, WeatherForecastRepository>(o => new WeatherForecastRepository(sqlConnectionString));
builder.Services.AddAuthorization();
builder.Services
    .AddIdentityApiEndpoints<IdentityUser>(options =>
    {
        options.User.RequireUniqueEmail = true;

    })
    .AddDapperStores(options =>
    {
        options.ConnectionString = sqlConnectionString;
    });
var app = builder.Build();

app.MapGet("/", () => $"The API is up . Connection string found: {(sqlConnectionStringFound ? "Found" : "Not Found")}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapGroup("/account")
    .MapIdentityApi<IdentityUser>();

app.MapPost("/account/logout",
    async (SignInManager<IdentityUser> signInManager,
    [FromBody] object empty) =>
    {
        if (empty != null)
        {
            await signInManager.SignOutAsync();
            return Results.Ok();
        }
        return Results.Unauthorized();
    }
    )
    .RequireAuthorization();
    

app.MapControllers()
    .RequireAuthorization();

app.Run();

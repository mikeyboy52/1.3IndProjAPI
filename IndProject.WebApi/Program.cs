using IndProject.WebApi.Controllers;
using IndProject.WebApi.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Adding Swagger Service for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlConnectionString = builder.Configuration.GetValue<string>("SqlConnectionString");
var sqlConnectionStringFound = !string.IsNullOrWhiteSpace(sqlConnectionString);
    builder.Services.AddTransient<EnviromentRepository, EnviromentRepository>(o => new EnviromentRepository(sqlConnectionString));
    builder.Services.AddTransient<Object2DRepository, Object2DRepository>(o => new Object2DRepository(sqlConnectionString));
builder.Services.AddAuthorization();
builder.Services
    .AddIdentityApiEndpoints<IdentityUser>(options =>
    {
        options.User.RequireUniqueEmail = true;

        options.Password.RequiredLength = 10;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;

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
    app.UseSwagger();
    app.UseSwaggerUI();
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

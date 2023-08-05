using IT3045C_FinalProject.Data;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApiDocument(document =>
{
    document.Title = "IT3045C Final Project";
    document.Description = "ASP.NET Core web API using NSwag and Entity Framework";
});

builder.Services.AddDbContext<FpDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("FpDatabase") ?? throw new InvalidOperationException("Connection string 'FPDatabase' not found.")));

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

app.UseOpenApi();
app.UseSwaggerUi3();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

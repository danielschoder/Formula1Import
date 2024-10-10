using Formula1.Application.Interfaces.Persistence;
using Formula1Import.Api.ServiceRegistrations;
using Formula1Import.Application.Handlers.QueryHandlers;
using Formula1Import.Infrastructure.Helpers;
using Formula1Import.Infrastructure.Middlewares;
using Formula1Import.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetVersion).Assembly));
builder.Services.AddExternalServices();
builder.Services.AddInfrastructureServices(builder.Environment, builder.Configuration);

var app = builder.Build();

app.UseMiddleware<GlobalHttpRequestMiddleware>();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapAllEndpoints();

app.UseHttpsRedirection();

app.Run();

public partial class Program { } // For NUnit WebApplication integration tests

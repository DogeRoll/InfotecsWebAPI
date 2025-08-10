using InfotecsWebApi.SepCsvParser;
using InfotecsWebAPI.Application;
using InfotecsWebAPI.Application.Exceptions;
using InfotecsWebAPI.Persistence;
using InfotecsWebAPI.Web.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddPersistenceInfrastructure(builder.Configuration);
}
catch (Exception ex)
{
    Log.Logger.Error($"Could not initialize service: {ex.Message}");
    Log.CloseAndFlush();
    return;
}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.SetupDatabase();

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

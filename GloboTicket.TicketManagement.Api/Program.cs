using GloboTicket.TicketManagement.Api;

using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("GlobalTicket Api starting");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration.
WriteTo.Console().
ReadFrom.Configuration(context.Configuration));

var app = builder
    .ConfigureService()
    .ConfigurePipeline();

app.UseSerilogRequestLogging();

await app.ResetDatabaseAsync();

app.Run();

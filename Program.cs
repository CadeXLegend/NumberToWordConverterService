using Serilog;
using NumberToWordConverterService.Services;
using NumberToWordConverterService.Middleware;
using NumberToWordConverterService.Services.Interfaces;
using NumberToWordConverterService.Types.Interfaces;
using NumberToWordConverterService.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile(
        "appsettings.json",
        optional: true,
        reloadOnChange: false)
    .AddEnvironmentVariables();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.AddSerilog(Log.Logger, dispose: true);
builder.WebHost.UseUrls("http://+:5021");
builder.Host.UseSerilog();

builder.Services.AddResponseCompression();
builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton(Log.Logger);

builder.Services.AddSingleton<INumericalSplitter<IPolyGraph<TriadicNumber>>, NumericalSplitter>();
builder.Services.AddSingleton<IPolyGraphAnalysisService, PolyGraphAnalysisService>();

var app = builder.Build();

app.UseMiddleware<InvalidRequestLoggingMiddleware>();

app.UseRouting();
app.UseResponseCompression();
app.MapControllers();

app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
});

app.Lifetime.ApplicationStopping.Register(Log.CloseAndFlush);
app.Run();

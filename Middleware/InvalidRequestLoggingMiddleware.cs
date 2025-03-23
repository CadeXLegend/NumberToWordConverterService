using NumberToWordConverterService.Constants;

namespace NumberToWordConverterService.Middleware;

public class InvalidRequestLoggingMiddleware(RequestDelegate next, Serilog.ILogger logger)
{
    private readonly RequestDelegate Next = next;
    private readonly Serilog.ILogger Logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        await Next(context);

        if (context.Items.TryGetValue("Outcome", out var outcome) &&
            outcome is InvalidRequestTypes outcomeValue and not 0)
        {
            Logger.Error("The request has failed due to: {outcome}", InvalidRequestResponses.Messages[outcomeValue]);
        }
    }
}
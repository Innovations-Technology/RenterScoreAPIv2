namespace RenterScoreAPIv2.ActionFilters;

using Microsoft.AspNetCore.Mvc.Filters;

public class LoggingActionFilter : Attribute, IActionFilter
{
    private readonly ILogger<LoggingActionFilter> _logger;

    public LoggingActionFilter(ILogger<LoggingActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var path = context.HttpContext.Request.Path;
        var httpMethod = context.HttpContext.Request.Method;
        var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
        _logger.LogInformation("[{Timestamp}][{httpMethod}] {path}", timestamp, httpMethod, path);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}

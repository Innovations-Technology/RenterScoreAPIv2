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
        _logger.LogInformation("[{httpMethod}] {path}", httpMethod, path);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}

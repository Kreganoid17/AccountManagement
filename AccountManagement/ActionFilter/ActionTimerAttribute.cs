using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace AccountManagement.ActionFilter;

public class ActionTimerAttribute(
    ILogger<ActionTimerAttribute> logger,
    Stopwatch stopwatch) : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        stopwatch.Restart();
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        stopwatch.Stop();

        logger.LogInformation(
        "Endpoint {Route} took {ElapsedMilliseconds}ms to execute",
            context.HttpContext.Request.Path, stopwatch.ElapsedMilliseconds);
    }
}

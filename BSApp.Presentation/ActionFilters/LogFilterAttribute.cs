using BSApp.Entities.LogModels;
using BSApp.Service.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using NLog.Fluent;

namespace BSApp.Presentation.ActionFilters;

public class LogFilterAttribute : ActionFilterAttribute
{
    private readonly ILoggerService _logger;

    public LogFilterAttribute(ILoggerService logger)
    {
        _logger = logger;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInfo(Log("OnActionExecuting", context.RouteData));
    }

    private string Log(string modelName, RouteData routeData)
    {
        var logDetail = new LogDetail(){
            ModelName = modelName,
            Controller = routeData.Values["controller"],
            Action = routeData.Values["Action"]
        };

        return logDetail.ToString();
    }
}

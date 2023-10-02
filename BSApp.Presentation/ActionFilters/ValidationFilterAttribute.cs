using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BSApp.Presentation.ActionFilters;

public class ValidationFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var controller = context.RouteData.Values["controller"];
        var action = context.RouteData.Values["action"];

        var param = context.ActionArguments.SingleOrDefault(p => p.Value.ToString().Contains("Dto")).Value; // get dto

        if(param is null){
            context.Result = new BadRequestObjectResult($"Object is cannot be null. controller: {controller}, action: {action}");
            return;
        }
        
        if (!context.ModelState.IsValid){
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }
        

    }
}

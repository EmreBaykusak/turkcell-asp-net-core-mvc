using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyAspNetCoreApp.Web.Filters;

public class LogFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        Debug.WriteLine("action method working stage");
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        Debug.WriteLine("action method finish stage");
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        Debug.WriteLine("action method result working stage");
    }

    public override void OnResultExecuted(ResultExecutedContext context)
    {
        Debug.WriteLine("action method result finish stage");
    }
}
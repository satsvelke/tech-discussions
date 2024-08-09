using corea.DependencyInj;
using Microsoft.AspNetCore.Mvc.Filters;

public class ActionFilter : IAsyncActionFilter
{

    private readonly string StoredProcedure;
    private readonly IServiceProvider serviceProvider;

    public ActionFilter(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        throw new NotImplementedException();
    }

    public async Task OnActionExecuting(ActionExecutingContext context)
    {
        // 
        // var p =   context.HttpContext.Request.Body ;

        var workflowcall = serviceProvider.GetService<IClassB>();

        var errors = await workflowcall?.Get("body", StoredProcedure);

        //if (errors is not null)
        // return BadRequest(errors);
    }

    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        throw new NotImplementedException();
    }
}
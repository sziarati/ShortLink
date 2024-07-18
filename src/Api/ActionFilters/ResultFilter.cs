using Application.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ResultFilter : ActionFilterAttribute
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        var result = context.Result;
        if (result is ObjectResult objectResult && objectResult.Value is AppResult appResult)
        {
            if (!appResult.IsSuccess)
            {
                context.Result = new BadRequestObjectResult(appResult.Message);
            }

            else if (result.GetType().IsAssignableTo(typeof(Result<>)))
            {
                var data = result?.GetType()?.GetProperty(nameof(Result<object>.Data))?.GetValue(result, null);
                context.Result = new OkObjectResult(data);
            }
        }
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
        // This method runs after the result is executed
    }
}
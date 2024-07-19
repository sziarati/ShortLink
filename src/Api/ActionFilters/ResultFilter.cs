using Application.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ResultFilter : ActionFilterAttribute
{
    public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
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

        return base.OnResultExecutionAsync(context, next);
    }
}
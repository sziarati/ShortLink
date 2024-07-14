using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Api.ExceptionHandler; //asp.net core 8 global exception handler

public class GlobalExceptionHandler(IHostEnvironment env, ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    private const string UnhandledExceptionMsg = "An unhandled exception has occurred while executing the request.";

    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = CreateProblemDetails(context, exception);
        var json = JsonSerializer.Serialize(problemDetails);

        logger.Log(LogLevel.Error, json);

        const string contentType = "application/problem+json";
        context.Response.ContentType = contentType;
        await context.Response.WriteAsync(json, cancellationToken);

        return true;
    }

    private ProblemDetails CreateProblemDetails(in HttpContext context, in Exception exception)
    {
        var statusCode = context.Response.StatusCode;
        var reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode);
        if (string.IsNullOrEmpty(reasonPhrase))
        {
            reasonPhrase = UnhandledExceptionMsg;
        }

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = reasonPhrase,
        };

        if (!env.IsDevelopment())
        {
            return problemDetails;
        }

        problemDetails.Detail = exception.ToString();
        problemDetails.Extensions["traceId"] = context.TraceIdentifier;
        problemDetails.Extensions["data"] = exception.Data;

        return problemDetails;
    }
}
using Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;

namespace Api.ExceptionHandler;

public class GlobalExceptionHandler(IHostEnvironment env, ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    private const string UnhandledExceptionMsg = "An unhandled exception has occurred while executing the request.";
    private const string ContentType = "application/problem+json";
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = CreateProblemDetails(context, exception);

        logger.LogError("{problemDetails}", JsonSerializer.Serialize(problemDetails));

        context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
        context.Response.ContentType = ContentType;
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private ProblemDetails CreateProblemDetails(in HttpContext context, in Exception exception)
    {
        if (exception is DomainException or ValidationException)
        {
            return new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = exception.Message,
            };
        }

        var statusCode = context.Response.StatusCode;
        var reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode);

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = !string.IsNullOrEmpty(reasonPhrase) ? reasonPhrase : UnhandledExceptionMsg,
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
using Application.Users.Create;
using Application.Users.Delete;
using Application.Users.Login;
using Application.Users.ResetPassword;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Users
{
    public static class UserEndPoints
    {
        public static IEndpointRouteBuilder MapUserEndPoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var scoped = endpointRouteBuilder.ServiceProvider.CreateScope();

            var mediator = scoped.ServiceProvider.GetService<IMediator>();

            if (mediator is null)
                return endpointRouteBuilder;

            endpointRouteBuilder.MapPost("/api/User", async (CreateUserCommand input) =>
            {
                var createResult = await mediator.Send(input);
                return Results.Ok(createResult);
            });

            endpointRouteBuilder.MapDelete("/api/User", async (DeleteUserCommand input) =>
            {
                var result = await mediator.Send(input);
                return Results.Ok(result);
            });

            endpointRouteBuilder.MapPut("/api/User", async ([FromBody] ResetPasswordCommand input) =>
            {
                var result = await mediator.Send(input);
                return Results.Ok(result);
            });

            endpointRouteBuilder.MapGet("/api/User", async ([FromBody] LoginUserCommand input) =>
            {
                var result = await mediator.Send(input);
                return Results.Ok(result);
            });

            return endpointRouteBuilder;
        }
    }
}
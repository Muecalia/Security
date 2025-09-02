using MediatR;
using Security.Application.Commands.Request.Account;
using Security.Application.Queries.Request.Account;

namespace Security.API.EndPoints
{
    public static class AccountEndPoint
    {
        public static void MapAccountEndPoint(this IEndpointRouteBuilder route)
        {
            route.MapGet("/account", async (IMediator mediator, CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10) =>
            {
                var result = await mediator.Send(new FindAllAccountsRequest(pageNumber, pageSize), cancellationToken);
                return Results.Ok(result);
            });

            route.MapGet("/account/{id}", async (string id, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new FindAccountByIdRequest(id), cancellationToken);
                return result.Succeeded ? Results.Ok(result) : Results.NotFound(result.Message);
            });

            route.MapGet("/account-by-group/{idGroup}", async (string idGroup, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new FindAccountByGroupRequest(idGroup), cancellationToken);
                return result.Succeeded ? Results.Ok(result) : Results.NotFound(result.Message);
            });

            route.MapPost("/account", async (CreateAccountRequest request, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(request, cancellationToken);
                return result.Succeeded ? Results.Created("", result) : Results.BadRequest(result.Message);
            });

            route.MapDelete("/account/{id}", async (string id, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new DeleteAccountRequest(id), cancellationToken);
                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result.Message);
            });

            route.MapPut("/account/{id}", async (string id, UpdateAccountRequest request, IMediator mediator, CancellationToken cancellationToken) =>
            {
                request.Id = id;
                var result = await mediator.Send(request, cancellationToken);
                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result.Message);
            });

            route.MapPut("/account/change-password/{id}", async (string id, ChangePasswordRequest request, IMediator mediator, CancellationToken cancellationToken) =>
            {
                request.Id = id;
                var result = await mediator.Send(request, cancellationToken);
                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result.Message);
            });
        }
    }
}

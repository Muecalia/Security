using MediatR;
using Security.Application.Commands.Request.Role;
using Security.Application.Queries.Request.Role;

namespace Security.API.EndPoints
{
    public static class RoleEndPoints
    {
        public static void MapRoleEndPoints(this IEndpointRouteBuilder route)
        {
            route.MapGet("/role", async (IMediator mediator, CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10) =>
            {
                var result = await mediator.Send(new FindAllRolesRequest(pageNumber, pageSize), cancellationToken);
                return Results.Ok(result);
            });

            route.MapGet("/role/{id}", async (string id, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new FindRoleByIdRequest(id), cancellationToken);
                return result.Succeeded ? Results.Ok(result) : Results.NotFound(result.Message);
            });

            route.MapPost("/role", async (RoleCreateRequest request, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(request, cancellationToken);
                return result.Succeeded ? Results.Created("", result) : Results.BadRequest(result.Message);
            });

            route.MapDelete("/role/{id}", async (string id, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new RoleDeleteRequest(id), cancellationToken);
                return result.Succeeded ? Results.NoContent() : Results.BadRequest(result.Message);
            });

            route.MapPut("/role/{id}", async (string id, RoleUpdateRequest request, IMediator mediator, CancellationToken cancellationToken) =>
            {
                request.Id = id;
                var result = await mediator.Send(request, cancellationToken);
                return result.Succeeded ? Results.NoContent() : Results.BadRequest(result.Message);
            });

        }

    }
}

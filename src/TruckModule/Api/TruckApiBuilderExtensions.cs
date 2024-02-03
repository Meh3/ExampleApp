using ErpApp.TruckModule.Application.Command;
using ErpApp.TruckModule.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ErpApp.TruckModule.Api;

public static class TruckApiBuilderExtensions
{
    public static void AddTruckModuleApi(this WebApplication app)
    {
        app.MapPost($"/trucks ", async (string code, string name, string? description, IMediator mediator, CancellationToken token) =>
        {
            var command = new CreateTruckCommand(code, name, description);
            var truckId = await mediator.Send(command, token);
            return Results.Created($"/trucks/{truckId}", truckId);
        }).WithOpenApi();

        app.MapDelete("/trucks/{id}", async (int id, IMediator mediator, CancellationToken token) =>
        {
            var command = new DeleteTruckCommand(id);
            await mediator.Send(command, token);
            return Results.NoContent();
        }).WithOpenApi();

        // only partial update allowed
        app.MapPatch("/trucks/{id}", async (int id, string code, string name, string? description, IMediator mediator, CancellationToken token) =>
        {
            var command = new EditTruckCommand(id, code, name, description);
            await mediator.Send(command, token);
            return Results.Ok();
        }).WithOpenApi();

        // not idempotent operation (can't be called multiple times)
        app.MapPost("/trucks/{id}/out-of-service", async (int id, IMediator mediator, CancellationToken token) =>
        {
            var command = new SetTruckOutOfServiceCommand(id);
            await mediator.Send(command, token);
            return Results.Ok();
        }).WithOpenApi();

        // not idempotent operation (can't be called multiple times)
        app.MapPost("/trucks/{id}/back-to-work/{status}", async (int id, TruckStatus status, IMediator mediator, CancellationToken token) =>
        {
            var command = new SetTruckBackToWorkCommand(id, status);
            await mediator.Send(command, token);
            return Results.Ok();
        }).WithOpenApi();

        // not idempotent operation (states changes every call)
        app.MapPost("/trucks/{id}/next-status", async (int id, IMediator mediator, CancellationToken token) =>
        {
            var command = new SetTruckNextStatusCommand(id);
            var newStatus = await mediator.Send(command, token);
            return Results.Ok(newStatus);
        }).WithOpenApi();
    }
}
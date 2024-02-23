using Application.Readers.Create;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Application.Readers.Get;

namespace Presentation;

public class ReaderModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("reader", async (CreateReaderCommand createReaderCommand, ISender sender) =>
        {
            await sender.Send(createReaderCommand);

            return Results.Created();
        });


        app.MapGet("reader/{id:guid}", async (Guid Id, ISender sender) =>
        {
            var query = new GetReaderQuery(Id);
            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.NotFound(result.Error);
            }

            return Results.Ok(result.Value);
        });
    }
}

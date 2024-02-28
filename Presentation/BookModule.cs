using Application.Books.Create;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Application.Books.GetByTitle;

namespace Presentation;

public class BookModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("book", async (CreateBookCommand request, ISender sender) =>
        {
            await sender.Send(request);

            return Results.Created();
        });

        app.MapGet("book", async (string name, ISender send) =>
        {
            var query = new GetBookByTitleQuery(name);
            var result = await send.Send(query);

            if (result.IsFailure)
            {
                return Results.NotFound();
            }

            return Results.Ok(result.Value);
        });
    }
}

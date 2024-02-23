using Application.Books.Create;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

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
    }
}

using Carter;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Application.Categories.Create;
using MediatR;
using Application.Categories.GetBookByCategory;

namespace Presentation;

public class CategoryModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("category", async (CreateCategoryCommand request, ISender sender) =>
        {
            await sender.Send(request);

            return Results.Created();
        });

        app.MapGet("category/{id:guid}/books", async (Guid id, ISender sender) =>
        {
            var query = new GetBookByCategoryQuery(id);

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.NotFound();
            }

            return Results.Ok(result.Value);
        });
    }
}

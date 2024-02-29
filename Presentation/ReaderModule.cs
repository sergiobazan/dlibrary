using Application.Readers.Create;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Application.Readers.Get;
using Application.Readers.NewLoan;
using Application.Readers.Return;
using Application.Readers.Login;
using Microsoft.AspNetCore.Authorization;

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


        app.MapGet("reader/{id:guid}", [Authorize] async (Guid Id, ISender sender) =>
        {
            var query = new GetReaderQuery(Id);
            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.NotFound(result.Error);
            }

            return Results.Ok(result.Value);
        });

        app.MapPost("reader/loan", async (NewLoanRequest request, ISender sender) =>
        {
            var command = new NewLoanCommand(request.ReaderId, request.BookId, request.NDays);
            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.NotFound(result.Error);
            }

            return Results.Ok(result.Value);
        });

        app.MapPost("reader/return", async (ReturnBookRequest request, ISender sender) =>
        {
            var command = new ReturnBookCommand(request.BookId);
            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.NotFound(result.Error);
            }

            return Results.Ok(result.Value);
        });

        app.MapPost("reader/login", async (LoginRequest request, ISender sender) =>
        {
            var command = new LoginCommand(request.Email);
            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.NotFound(result.Error);
            }

            return Results.Ok(result.Value);
        });
    }
}

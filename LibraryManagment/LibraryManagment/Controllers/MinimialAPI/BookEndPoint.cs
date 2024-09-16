using FluentValidation;
using Library.Application.Commands.Books;
using Library.Application.DTO;
using Library.Domain.Entites.BookAggregate;
using Library.Domain.Interface;
using Library.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibraryManagment.Controllers.MinimialAPI;
public static class BookEndPoint
{
    public static void MapEndpoint(this IEndpointRouteBuilder app)
    {
        //ADD
        app.MapPost("/api/createBook/", async (
               [FromServices] IMediator mediatr,
                 [FromServices] ILogger<UnitOfWork> logger,
                 [FromBody] CreateBookCommand model,
                 [FromServices] IValidator<CreateBookCommand> validator, CancellationToken cancellationToken) =>
        {

            ApiResponse response = new();

            try
            {
                logger.LogInformation("Processing book creation");

                var validationResult = await validator.ValidateAsync(model);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors);
                }

                var result = await mediatr.Send(model, cancellationToken);  //controller send Command to Mediatr instance, and Mediatr route Command to correspanding Handler;

                if (result == false)
                {
                    response.Errors.Add(new Exception("error occured").ToString());
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                }

                response.IsSuccess = true;
                response.Result = result;
                response.StatusCode = HttpStatusCode.OK;

                return Results.Ok(response.Result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating a book");
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        })
 .WithName("CreatedBook")
 .Produces<ApiResponse>(200)
 .Produces<ApiResponse>(400)
 .Produces<ApiResponse>(500)
 .WithTags("Books");


        //UPDATE

        app.MapPut("/api/updateBook/", async (
                [FromServices] IMediator mediatr,
                 [FromServices] ILogger<UnitOfWork> logger,
                 [FromBody] UpdateBookCommand model,
                 [FromServices] IValidator<UpdateBookCommand> validator, CancellationToken cancellationToken) =>
        {

            ApiResponse response = new();

            try
            {
                logger.LogInformation("Processing book update");

                var validationResult = await validator.ValidateAsync(model);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors);
                }

                //all Edit,repostitory,unitofwork are handled in UpdateCommandHandler
                var result = await mediatr.Send(model, cancellationToken);

                if (result == Guid.Empty)
                {
                    response.Errors.Add(new Exception("error occured").ToString());
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                }

                response.IsSuccess = true;
                response.Result = result;
                response.StatusCode = HttpStatusCode.OK;

                return Results.Ok(response.Result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while update a book");
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        })
 .WithName("UpdatedBook")
 .Produces<ApiResponse>(200)
 .Produces<ApiResponse>(400)
 .Produces<ApiResponse>(500)
 .WithTags("Books");


        //DELETE

        app.MapDelete("/api/deleteBook/", async (
           [FromServices] IMediator mediatr,
            [FromServices] ILogger<IMediator> logger,
            [FromBody] DeleteBookCommand model,
            [FromServices] IValidator<DeleteBookCommand> validator, CancellationToken cancellationToken) =>
        {

            ApiResponse response = new();

            try
            {
                logger.LogInformation("Processing book delete");

                var validationResult = await validator.ValidateAsync(model);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors);
                }

                var result = await mediatr.Send(model, cancellationToken);

                if (result == Guid.Empty)
                {
                    response.Errors.Add(new Exception("error occured").ToString());
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                }

                response.IsSuccess = true;
                response.Result = result;
                response.StatusCode = HttpStatusCode.OK;

                return Results.Ok(response.Result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while update a book");

                response.IsSuccess = false;
                response.Errors.Add(ex.Message);  // Add the error message to the response
                response.StatusCode = HttpStatusCode.InternalServerError;

                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        })
.WithName("DeleteBook")
.Produces<ApiResponse>(200)
.Produces<ApiResponse>(400)
.Produces<ApiResponse>(500)
.WithTags("Books");

        //GETALL


        //GETBYID


    }
}
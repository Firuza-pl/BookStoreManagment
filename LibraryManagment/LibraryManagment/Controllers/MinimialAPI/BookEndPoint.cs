﻿using Azure;
using FluentValidation;
using Library.Application.Commands.Books;
using Library.Application.DTO;
using Library.Application.Queries.Books;
using Library.Domain.Entites.BookAggregate;
using Library.Domain.Events;
using Library.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace LibraryManagment.Controllers.MinimialAPI;
public static class BookEndPoint
{
    public static void MapBookEndpoint(this IEndpointRouteBuilder app)
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

        //GETALL-non and active
        app.MapGet("/api/getAllBook/", async (
            [FromServices] ILogger<IBookQueries> logger,
            IBookQueries queries) =>
        {

            ApiResponse response = new();

            try
            {
                logger.LogInformation("Processing book getting");

                var result = await queries.GetAllAsync();

                if (result is null)
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
                logger.LogError(ex, "An error occurred while getting all a book");

                response.IsSuccess = false;
                response.Errors.Add(ex.Message);  // Add the error message to the response
                response.StatusCode = HttpStatusCode.InternalServerError;

                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        })
.WithName("GetNonActiveBook")
.Produces<ApiResponse>(200)
.Produces<ApiResponse>(400)
.Produces<ApiResponse>(500)
.WithTags("Books");


        //GetActive
        app.MapGet("/api/getActiveBook/", async (
            [FromServices] ILogger<IBookQueries> logger,
            IBookQueries queries) =>
        {

            ApiResponse response = new();

            try
            {
                logger.LogInformation("Processing getting only active book");

                var result = await queries.GetActiveAsync();

                if (result is null)
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
                logger.LogError(ex, "An error occurred while getting only active book");

                response.IsSuccess = false;
                response.Errors.Add(ex.Message);  // Add the error message to the response
                response.StatusCode = HttpStatusCode.InternalServerError;

                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        })
.WithName("GetAllActiveBook")
.Produces<ApiResponse>(200)
.Produces<ApiResponse>(400)
.Produces<ApiResponse>(500)
.WithTags("Books");


        //GETBYID
        app.MapGet("/api/getSingleBook/{id:Guid}", async (Guid id,
        [FromServices] ILogger<IBookQueries> logger,
        IBookQueries queries) =>
        {

            ApiResponse response = new();

            try
            {
                logger.LogInformation("Processing book getting");

                var result = await queries.GetByIdAsync(id);

                if (result is null)
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
                logger.LogError(ex, "An error occurred while getting single a book");

                response.IsSuccess = false;
                response.Errors.Add(ex.Message);  // Add the error message to the response
                response.StatusCode = HttpStatusCode.InternalServerError;

                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        })
.WithName("GetByIdBook")
.Produces<ApiResponse>(200)
.Produces<ApiResponse>(400)
.Produces<ApiResponse>(500)
.WithTags("Books");



        //bookborrowing

        app.MapPost("/api/borrowBook", async (
             [FromServices] ILogger<UnitOfWork> logger,
    [FromServices] IMediator mediator,
    [FromBody] CreateBorrowBookCommand command,
    CancellationToken cancellationToken) =>
        {
            ApiResponse response = new();

            try
            {

                logger.LogInformation("Processing book borrowing");

                var result = await mediator.Send(command, cancellationToken);

                if (result is false)
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
                logger.LogError(ex, "An error occurred while borrowing a book");

                response.IsSuccess = false;
                response.Errors.Add(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;

                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        })
.WithName("borrowBook")
.Produces<ApiResponse>(200)
.Produces<ApiResponse>(400)
.Produces<ApiResponse>(500)
.WithTags("Books");


    }
}
using FluentValidation;
using Library.Application.Commands.BorrowRecords;
using Library.Application.DTO;
using Library.Application.Queries.BorrowRecords;
using Library.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace LibraryManagment.Controllers.MinimialAPI
{
    public static class RecordEndPoint
    {
        public static void MapRecordEndpoint(this IEndpointRouteBuilder app)
        {
            //ADD
            app.MapPost("/api/createRecord/", async (
                   [FromServices] IMediator mediatr,
                     [FromServices] ILogger<UnitOfWork> logger,
                     [FromBody] CreateRecordCommand model,
                     [FromServices] IValidator<CreateRecordCommand> validator, CancellationToken cancellationToken) =>
            {

                ApiResponse response = new();

                try
                {
                    logger.LogInformation("Processing book borrowing");

                    var validationResult = await validator.ValidateAsync(model);
                    if (!validationResult.IsValid)
                    {
                        return Results.BadRequest(validationResult.Errors);
                    }

                    var result = await mediatr.Send(model, cancellationToken); 

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
                    logger.LogError(ex, "An error occurred while creating a record");
                    return Results.StatusCode((int)HttpStatusCode.InternalServerError);
                }
            })
     .WithName("CreatedRecord")
     .Produces<ApiResponse>(200)
     .Produces<ApiResponse>(400)
     .Produces<ApiResponse>(500)
     .WithTags("Borrow a book");


            //UPDATE
            app.MapPut("/api/updateRecord/", async (
                    [FromServices] IMediator mediatr,
                     [FromServices] ILogger<UnitOfWork> logger,
                     [FromBody] UpdateRecordCommand model,
                     [FromServices] IValidator<UpdateRecordCommand> validator, CancellationToken cancellationToken) =>
            {

                ApiResponse response = new();

                try
                {
                    logger.LogInformation("Processing book borrowing");

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
                    logger.LogError(ex, "An error occurred while update a record");
                    return Results.StatusCode((int)HttpStatusCode.InternalServerError);
                }
            })
     .WithName("UpdatedRecord")
     .Produces<ApiResponse>(200)
     .Produces<ApiResponse>(400)
     .Produces<ApiResponse>(500)
     .WithTags("Borrow a book");


            //DELETE
            app.MapDelete("/api/deleteRecord/", async (
               [FromServices] IMediator mediatr,
                [FromServices] ILogger<IMediator> logger,
                [FromBody] DeleteRecordCommand model,
                [FromServices] IValidator<DeleteRecordCommand> validator, CancellationToken cancellationToken) =>
            {

                ApiResponse response = new();

                try
                {
                    logger.LogInformation("Processing book borrowing");

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
                    logger.LogError(ex, "An error occurred while deleting a record");

                    response.IsSuccess = false;
                    response.Errors.Add(ex.Message);  // Add the error message to the response
                    response.StatusCode = HttpStatusCode.InternalServerError;

                    return Results.StatusCode((int)HttpStatusCode.InternalServerError);
                }
            })
    .WithName("DeleteRecord")
    .Produces<ApiResponse>(200)
    .Produces<ApiResponse>(400)
    .Produces<ApiResponse>(500)
    .WithTags("Borrow a book");


            //GetAll
            app.MapGet("/api/getAllRecord/", async (
                [FromServices] ILogger<IRecordQueries> logger,
                IRecordQueries queries) =>
            {

                ApiResponse response = new();

                try
                {
                    logger.LogInformation("Processing getting recording");

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
                    logger.LogError(ex, "An error occurred while getting record");

                    response.IsSuccess = false;
                    response.Errors.Add(ex.Message);  // Add the error message to the response
                    response.StatusCode = HttpStatusCode.InternalServerError;

                    return Results.StatusCode((int)HttpStatusCode.InternalServerError);
                }
            })
    .WithName("GetAllRecord")
    .Produces<ApiResponse>(200)
    .Produces<ApiResponse>(400)
    .Produces<ApiResponse>(500)
    .WithTags("Borrow a book");


            //GETBYID
            app.MapGet("/api/getSingleRecord/{id:Guid}", async (Guid id,
            [FromServices] ILogger<IRecordQueries> logger,
            IRecordQueries queries) =>
            {

                ApiResponse response = new();

                try
                {
                    logger.LogInformation("Processing book borrowing");

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
                    logger.LogError(ex, "An error occurred while getting single a record");

                    response.IsSuccess = false;
                    response.Errors.Add(ex.Message); 
                    response.StatusCode = HttpStatusCode.InternalServerError;

                    return Results.StatusCode((int)HttpStatusCode.InternalServerError);
                }
            })
    .WithName("GetByIdRecord")
    .Produces<ApiResponse>(200)
    .Produces<ApiResponse>(400)
    .Produces<ApiResponse>(500)
    .WithTags("Borrow a book");


        }
    }
}

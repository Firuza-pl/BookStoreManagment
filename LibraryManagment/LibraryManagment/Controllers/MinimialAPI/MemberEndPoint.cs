using FluentValidation;
using Library.Application.Commands.Books;
using Library.Application.Commands.Members;
using Library.Application.DTO;
using Library.Application.Queries.Books;
using Library.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibraryManagment.Controllers.MinimialAPI;
public static class MemberEndPoint
{
    public static void MapMemberEndpoint(this IEndpointRouteBuilder app)
    {
        //ADD
        app.MapPost("/api/createMember/", async (
               [FromServices] IMediator mediatr,
                 [FromServices] ILogger<UnitOfWork> logger,
                 [FromBody] CreateMemberCommand model,
                 [FromServices] IValidator<CreateMemberCommand> validator, CancellationToken cancellationToken) =>
        {

            ApiResponse response = new();

            try
            {
                logger.LogInformation("Processing member creation");

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
                logger.LogError(ex, "An error occurred while creating a member");
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        })
 .WithName("CreatedMember")
 .Produces<ApiResponse>(200)
 .Produces<ApiResponse>(400)
 .Produces<ApiResponse>(500)
 .WithTags("Members");


        //UPDATE

        app.MapPut("/api/updateMember/", async (
                [FromServices] IMediator mediatr,
                 [FromServices] ILogger<UnitOfWork> logger,
                 [FromBody] UpdateMemberCommand model,
                 [FromServices] IValidator<UpdateMemberCommand> validator, CancellationToken cancellationToken) =>
        {

            ApiResponse response = new();

            try
            {
                logger.LogInformation("Processing Member update");

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
                logger.LogError(ex, "An error occurred while update a Member");
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        })
 .WithName("UpdatedMember")
 .Produces<ApiResponse>(200)
 .Produces<ApiResponse>(400)
 .Produces<ApiResponse>(500)
 .WithTags("Members");


        //DELETE

        app.MapDelete("/api/deleteMember/", async (
           [FromServices] IMediator mediatr,
            [FromServices] ILogger<IMediator> logger,
            [FromBody] DeleteMemberCommand model,
            [FromServices] IValidator<DeleteMemberCommand> validator, CancellationToken cancellationToken) =>
        {

            ApiResponse response = new();

            try
            {
                logger.LogInformation("Processing Member delete");

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
                logger.LogError(ex, "An error occurred while update a Member");

                response.IsSuccess = false;
                response.Errors.Add(ex.Message);  // Add the error message to the response
                response.StatusCode = HttpStatusCode.InternalServerError;

                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        })
.WithName("DeleteMember")
.Produces<ApiResponse>(200)
.Produces<ApiResponse>(400)
.Produces<ApiResponse>(500)
.WithTags("Members");

        //GETALL-non and active

        app.MapGet("/api/getAllMember/", async (
            [FromServices] ILogger<IBookQueries> logger,
            IBookQueries queries) =>
        {

            ApiResponse response = new();

            try
            {
                logger.LogInformation("Processing Member getting");

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
                logger.LogError(ex, "An error occurred while getting all a Member");

                response.IsSuccess = false;
                response.Errors.Add(ex.Message);  // Add the error message to the response
                response.StatusCode = HttpStatusCode.InternalServerError;

                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        })
.WithName("GetNonActiveMember")
.Produces<ApiResponse>(200)
.Produces<ApiResponse>(400)
.Produces<ApiResponse>(500)
.WithTags("Members");


        //GetActive

        app.MapGet("/api/getActiveMember/", async (
            [FromServices] ILogger<IBookQueries> logger,
            IBookQueries queries) =>
        {

            ApiResponse response = new();

            try
            {
                logger.LogInformation("Processing getting only active Member");

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
                logger.LogError(ex, "An error occurred while getting only active Member");

                response.IsSuccess = false;
                response.Errors.Add(ex.Message);  // Add the error message to the response
                response.StatusCode = HttpStatusCode.InternalServerError;

                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        })
.WithName("GetAllActiveMember")
.Produces<ApiResponse>(200)
.Produces<ApiResponse>(400)
.Produces<ApiResponse>(500)
.WithTags("Members");



        //GETBYID
        app.MapGet("/api/getSingleMember/{id:Guid}", async (Guid id,
        [FromServices] ILogger<IBookQueries> logger,
        IBookQueries queries) =>
        {

            ApiResponse response = new();

            try
            {
                logger.LogInformation("Processing Member getting");

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
                logger.LogError(ex, "An error occurred while getting single a Member");

                response.IsSuccess = false;
                response.Errors.Add(ex.Message);  // Add the error message to the response
                response.StatusCode = HttpStatusCode.InternalServerError;

                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        })
.WithName("GetByIdMember")
.Produces<ApiResponse>(200)
.Produces<ApiResponse>(400)
.Produces<ApiResponse>(500)
.WithTags("Members");


    }
}

using FluentValidation;
using Library.Application.Commands.Books;
using Library.Application.DTO;
using Library.Domain.Entites.BookAggregate;
using Library.Domain.Interface;
using Library.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibraryManagment.Controllers.MinimialAPI;
public static class BookEndPoint
{
    public static void MapEndpoint(this IEndpointRouteBuilder app)
    {
        //ADD
        app.MapPost("/api/createBook/", async (
                 [FromServices] IUnitOfWork unitOfWork,
                 [FromServices] ILogger<UnitOfWork> logger,
                 [FromBody] CreateBookCommand model,
                 [FromServices] IValidator<CreateBookCommand> validator) =>
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

                var book = new Book(model.Id, model.Title);

                // Add book to the repository via UnitOfWork
                var result = await unitOfWork.BookRepository.AddAsync(book);

                if(result is null)
                {
                    response.Errors.Add(new Exception("error occured").ToString());
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                }

                // Commit transaction
                await unitOfWork.Save();

                response.IsSuccess = true;
                response.Result = book;
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


        //DELETE


        //GETALL


        //GETBYID


    }
}
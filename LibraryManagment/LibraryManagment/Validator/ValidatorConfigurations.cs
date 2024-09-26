using FluentValidation;
using Library.Application.Commands.Books;
using Library.Application.Commands.Members;

namespace LibraryManagment.Validator;
public static class ValidatorConfigurations
{
    public static void AddValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateBookCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateBookCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<DeleteBookCommandValidator>();

        services.AddValidatorsFromAssemblyContaining<CreateMemberCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateMemberCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<DeleteMemberCommandValidator>();

    }
}

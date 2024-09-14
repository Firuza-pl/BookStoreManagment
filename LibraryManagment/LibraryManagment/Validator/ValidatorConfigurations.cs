using FluentValidation;
using Library.Application.Commands.Books;

namespace LibraryManagment.Validator;
public static class ValidatorConfigurations
{
    public static void AddValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateBookCommandValidator>();
    }
}

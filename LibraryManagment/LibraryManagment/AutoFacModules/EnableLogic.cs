using Library.Domain.Entites.BookAggregate;
using Library.Domain.Interface;
using Library.Infrastructure.Repositories;

namespace LibraryManagment.Dependency;
public static class EnableLogic
{
    public static void LoadModule(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        //for book 
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IGenericRepository<Book>, GenericRepository<Book>>();

        //for member

    }
}

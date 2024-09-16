using Library.Application.Commands.Books;
using Library.Application.Queries.Books;
using Library.Domain.Interface;
using Library.Infrastructure.Idempotency;
using Library.Infrastructure.Repositories;

namespace LibraryManagment.Dependency;
public static class EnableLogic
{
    public static void LoadModule(this IServiceCollection services)
    {
        //BOOK
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(CreateBookCommandHandler).Assembly);
            cfg.RegisterServicesFromAssemblies(typeof(UpdateBookCommandHandler).Assembly);
            cfg.RegisterServicesFromAssemblies(typeof(DeleteBookCommandHandler).Assembly);
        });

        //repos for general
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IRequestManager, RequestManager>();

        //command for book
        services.AddScoped<IBookRepository, BookRepository>();

        //queries for book
        services.AddScoped<IBookQueries, BookQueries>();


    }
}

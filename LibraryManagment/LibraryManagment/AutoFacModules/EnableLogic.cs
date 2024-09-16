using Library.Application.Commands.Books;
using Library.Domain.Interface;
using Library.Infrastructure.Idempotency;
using Library.Infrastructure.Repositories;

namespace LibraryManagment.Dependency;
public static class EnableLogic
{
    public static void LoadModule(this IServiceCollection services)
    {

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(CreateBookCommandHandler).Assembly);
            cfg.RegisterServicesFromAssemblies(typeof(UpdateBookCommandHandler).Assembly);
            cfg.RegisterServicesFromAssemblies(typeof(DeleteBookCommandHandler).Assembly);
        });


        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IRequestManager, RequestManager>();

        //for book 
        services.AddScoped<IBookRepository, BookRepository>();

        //for member

    }
}

using Library.Application.Services;
using Library.Application.Services.Interfaces;
using Library.Domain.Interface;
using Library.Infrastructure.Repositories;

namespace LibraryManagment.Dependency;
public static class EnableLogic
    {
        public static void LoadModule(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBookService, BookService>();
        }
    }

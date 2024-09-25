﻿using Library.Application.Commands.Books;
using Library.Application.Commands.BorrowRecords;
using Library.Application.Commands.Members;
using Library.Application.Queries.Books;
using Library.Application.Queries.BorrowRecords;
using Library.Application.Queries.Members;
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

            cfg.RegisterServicesFromAssemblies(typeof(CreateMemberCommandHandler).Assembly);
            cfg.RegisterServicesFromAssemblies(typeof(UpdateMemberCommandHandler).Assembly);
            cfg.RegisterServicesFromAssemblies(typeof(DeleteMemberCommandHandler).Assembly);

            cfg.RegisterServicesFromAssemblies(typeof(CreateRecordCommandHandler).Assembly);
            cfg.RegisterServicesFromAssemblies(typeof(UpdateRecordCommandHandler).Assembly);
            cfg.RegisterServicesFromAssemblies(typeof(DeleteRecordCommandHandler).Assembly);
        });

        //repos for general
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IRequestManager, RequestManager>();

        //command 
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IBorrowBookRepository, BorrowBookRepository>();


        //queries 
        services.AddScoped<IBookQueries, BookQueries>();
        services.AddScoped<IMemberQueries, MemberQueries>();
        services.AddScoped<IRecordQueries, RecordQueries>();



    }
}

using Library.Application.Commands.Books;
using Library.Domain.Entites.MemberAggregate;
using Library.Infrastructure.Persistence;
using LibraryManagment.Controllers.MinimialAPI;
using LibraryManagment.Dependency;
using LibraryManagment.Mapper;
using LibraryManagment.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Register MediatR and specify the assembly to scan
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddSingleton(AutoMapperConfig.CreateMapper());
builder.Services.AddValidator();
builder.Services.LoadModule();
//EnableLogic.LoadModule(builder.Services);

builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // or any other logging provider


var app = builder.Build();

// Call seed method after building the app
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await AppDbContextSeed.SeedDatabaseAsync(services);
}

///register Endpoints
try
{
    app.MapBookEndpoint();
    app.MapMemberEndpoint();

}
catch (Exception ex)
{
    throw;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();

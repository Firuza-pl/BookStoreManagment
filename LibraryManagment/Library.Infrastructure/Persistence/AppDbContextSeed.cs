using Library.Domain.Entites.BookAggregate;
using Library.Domain.Entites.MemberAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Infrastructure.Persistence
{
    public static class AppDbContextSeed
    {
        private const string UserEmail = "test@test.com";
        private const string UserPassword = "Test123*";
        private const string RoleName = "administrator";

        public static async Task SeedDatabaseAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                context.Database.EnsureCreated(); //db is creaated

                // Seed roles
                const string RoleName = "Admin";
                const string UserEmail = "admin@example.com";
                const string UserPassword = "Admin@123";

                if (!await roleManager.RoleExistsAsync(RoleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(RoleName));
                }

                if (await userManager.FindByEmailAsync(UserEmail) == null)
                {
                    var user = new ApplicationUser { UserName = UserEmail, Email = UserEmail, EmailConfirmed = true };
                    await userManager.CreateAsync(user, UserPassword);
                    await userManager.AddToRoleAsync(user, RoleName);
                }

                // Seed other entities like Book, Member
                if (!context.Books.Any())
                {
                    List<Book> books = new List<Book>
            {
            };
                    context.Books.AddRange(books);
                }

                if (!context.Memberships.Any())
                {
                    List<Member> members = new List<Member>
            {
                new Member( "Member1"),
                new Member( "Member2"),
                new Member( "Member3"),
            };
                    context.Memberships.AddRange(members);
                }

                // Save changes to database
                await context.SaveChangesAsync();
            }
        }
    }
}

using Library.Domain.Entites;
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
            using (AppDbContext context = serviceProvider.GetRequiredService<AppDbContext>())
            {
                UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(RoleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(RoleName));
                }

                if (await userManager.FindByEmailAsync(UserEmail) == null)
                {
                    ApplicationUser user = new ApplicationUser { UserName = UserEmail, Email = UserEmail, EmailConfirmed = true };
                    await userManager.CreateAsync(user, UserPassword);
                    await userManager.AddToRoleAsync(user, RoleName);
                }

                List<Book> book = new List<Book>() {
                new Book(Guid.NewGuid(), "Book1",true),
                new Book(Guid.NewGuid(), "Book2",true),
                new Book(Guid.NewGuid(), "Book3",true),
                };

                List<Member> member = new List<Member>() {
                new Member(Guid.NewGuid(), "Member1"),
                new Member(Guid.NewGuid(), "Member2"),
                new Member(Guid.NewGuid(), "Member3"),
                };

                List<Member> borrowMember = new List<Member>() {
                new Member(Guid.NewGuid(), "Member1"),
                new Member(Guid.NewGuid(), "Member2"),
                new Member(Guid.NewGuid(), "Member3"),
                };
            }
        }
    }
}

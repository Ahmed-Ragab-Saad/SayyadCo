using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Enums;
using SayyadCo.Domain.Interfaces;
using SayyadCo.Infrastructure.Identity;

namespace SayyadCo.Infrastructure.Data.Seeders
{
    public class Seeder
    {
        private readonly IServiceProvider _serviceProvider;

        public Seeder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task SeedAsync()
        {
            await SeedRolesAsync();
            await SeedSuperAdminAsync();
            await SeedFunnyGamesSction();
        }

        private async Task SeedRolesAsync()
        {
            var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = [AppRoles.SuperAdmin, AppRoles.Admin, AppRoles.User];

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedSuperAdminAsync()
        {
            var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var email = "superadmin@sayyadco.com";

            var existingUser = await userManager.FindByEmailAsync(email);
            if (existingUser is not null)
                return;

            var user = new ApplicationUser
            {
                FirstName = "Super",
                LastName = "Admin",
                Email = email,
                UserName = email,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user, "12345678");
            await userManager.AddToRoleAsync(user, AppRoles.SuperAdmin);
        }

        private async Task SeedFunnyGamesSction()
        {
            var unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();

            var funnySection = await unitOfWork.Sections.GetFunnySection();
            if (funnySection is null)
            {
                var newFunnySection = new Section()
                {
                    TitleAr = "العاب ترفيهية",
                    TitleEn = "Funny games",
                    DescriptionAr = "العاب ترفيهية",
                    DescriptionEn = "Funny games",
                    SectionType = SectionType.Funny
                };

                await unitOfWork.Sections.AddAsync(newFunnySection);
                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}

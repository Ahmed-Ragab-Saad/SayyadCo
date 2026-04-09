using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SayyadCo.Domain.Common;

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
    }
}

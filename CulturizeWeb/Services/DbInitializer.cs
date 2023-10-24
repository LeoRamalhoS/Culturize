
using Culturize.Models;
using Culturize.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace CulturizeWeb.Services
{
    public class DbInitializerService : IDbInitializerService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<DbInitializerService> _logger;

        public DbInitializerService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IUserStore<IdentityUser> userStore, ILogger<DbInitializerService> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userStore = userStore;
            _logger = logger;
            _emailStore = GetEmailStore();
        }

        public async Task<IdentityRole?> SeedDataBase()
        {
            try
            {
                _logger.LogInformation("Seed Database initializing...");
                var roleExists = await _roleManager.Roles.FirstOrDefaultAsync();

                //Seed roles
                if (roleExists == null)
                {
                    _logger.LogInformation("Seeding roles to Database...");

                    await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
                    await _roleManager.CreateAsync(new IdentityRole(SD.Role_CompanyAdmin));
                    await _roleManager.CreateAsync(new IdentityRole(SD.Role_Leader));
                    await _roleManager.CreateAsync(new IdentityRole(SD.Role_User));

                    _logger.LogInformation("Roles created successfully !");

                    _logger.LogInformation("Seeding admin data...");

                    //Seed admin data
                    var adminUser = Activator.CreateInstance<ApplicationUser>();
                    var adminEmail = "admin@admin.com"; //TODO: LOAD A REAL INITIAL ADMIN EMAIL

                    await _userStore.SetUserNameAsync(adminUser, adminEmail, CancellationToken.None);
                    await _emailStore.SetEmailAsync(adminUser, adminEmail, CancellationToken.None);
                    var result = await _userManager.CreateAsync(adminUser, "Admin@123");

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("Admin user created successfully !");

                        _logger.LogInformation("Linking admin user with admin role...");

                        await _userManager.AddToRoleAsync(adminUser, SD.Role_Admin);

                        _logger.LogInformation("Admin seeded successfully !");
                    }
                }

                _logger.LogInformation("Seed Database finished...");
                return roleExists;
            }
            catch(Exception ex)
            {
                throw(ex);
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}

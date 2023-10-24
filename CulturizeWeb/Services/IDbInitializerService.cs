using Microsoft.AspNetCore.Identity;

namespace CulturizeWeb.Services
{
    public interface IDbInitializerService
    {
        Task<IdentityRole?> SeedDataBase();
    }
}
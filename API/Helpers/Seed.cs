using API.BLL.Entities.Identity;
using API.DAL.Data;
using API.DAL.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers
{
    public class Seed
    {
       public static async void SeedData(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerfactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<StoreContext>();
                    await context.Database.MigrateAsync();
                    await StoreContextSeed.SeedAsync(context, loggerfactory);
                    var usermanger = services.GetRequiredService<UserManager<AppUser>>();
                    var identityContext=services.GetRequiredService<AppIdentityDbContext>();
                    await identityContext.Database.MigrateAsync();
                    await AppIdentityDbContextSeed.SeedUserAsync(usermanger);          
                }
                catch (Exception)
                {
                    //var logger = LoggerFactory.CreateLogger<Program>();
                    //logger.LogError(ex.Message);
                    throw;
                }
            }
        }
    }
}

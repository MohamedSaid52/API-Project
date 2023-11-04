using API.BLL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DAL.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Mohamed",
                    Email = "Mohamed@gmail.com",
                    UserName = "mohamed@gmail.com",
                    Adress = new Adress
                    {
                        FirstName = "Mohamed",
                        LastName = "Saeed",
                        sreet = "kafrHegazy",
                        City = "qanater",
                        State = "Giza",
                        ZipCode = "12952"
                    }
                };  
                    await userManager.CreateAsync(user);
            }
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreIdentityContextSeed
    {

        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Badry Anas",
                    Email = "badryanas@gmail.com",
                    UserName = "BadryAnas", 
                    Address =
                    {
                        FirstName = "Mohamed", 
                        LastName = "Moaauid", 
                        PostalCode = "12345", 
                        City = "Giza", 
                        Street ="Monira", 
                        state = "Giza"
                    }
                };

                await userManager.CreateAsync(user , "1234Bb!");
            }
            
        }



    }
}

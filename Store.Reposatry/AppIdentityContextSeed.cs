using Microsoft.AspNetCore.Identity;
using Store.Data.Entites.IdentitEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Reposatry
{
    public class AppIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any() )
            {
                var user = new AppUser 
                { 
                    DisplayName="mona yousef",
                    Email="mona@gmail.com",
                    UserName="mona",
                    Address=new Address
                    {
                        FirstName="mona",
                        LastName="yousef",
                        City="cairo",
                        State="cairo",
                        Street="77",
                        ZipCode="12345"
                       
                    }               
                };
                await userManager.CreateAsync(user,"Password123!");

            }
        }
    }
}

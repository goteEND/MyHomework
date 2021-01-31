using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyHomework.API.Entities;
using MyHomework.API.Persistence;
using Newtonsoft.Json;

namespace MyHomework.API.Helpers
{
    public class DbInitializer
    {
        public static async Task SeedData(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            DataContext dataContext)
        {
            if (await userManager.Users.AnyAsync())
                return;

            var userData = await File.ReadAllTextAsync("Helpers/DataForSeed/Users.json");
            var users = JsonConvert.DeserializeObject<List<dynamic>>(userData);
            if (users == null)
                return;

            var roles = new List<AppRole>
            {
                new AppRole {Name = "Student"},
                new AppRole {Name = "Professor"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var dynamicUser in users)
            {

                string userName = Convert.ToString(dynamicUser.UserName);
                var user = new AppUser
                {
                    UserName = userName.ToLower(),
                    FirstName = Convert.ToString(dynamicUser.FirstName),
                    LastName = Convert.ToString(dynamicUser.LastName),
                    Email = (Convert.ToString(dynamicUser.Email) + "@gmail.com")
                };

                await userManager.CreateAsync(user, "password");

            
                await userManager.AddToRoleAsync(user, Convert.ToString(dynamicUser.UserRoleForSeed));
            }




            var subjectsData = await File.ReadAllTextAsync("Helpers/DataForSeed/Subjects.json");
            var subjects = JsonConvert.DeserializeObject<List<Subject>>(subjectsData);
          
            await dataContext.Subjects.AddRangeAsync(subjects);
            await dataContext.SaveChangesAsync();


            var projectsData = await File.ReadAllTextAsync("Helpers/DataForSeed/Projects.json");
            var projects = JsonConvert.DeserializeObject<List<Project>>(projectsData);

            await dataContext.Projects.AddRangeAsync(projects);
            await dataContext.SaveChangesAsync();
        }
    }
}   

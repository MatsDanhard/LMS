using System.Collections.Generic;
using LmsTool.Models;
using LmsTool.Models.DbModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LmsTool.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LmsTool.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LmsTool.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);

            string[] roleNames = new[] {"Teacher", "Student"};
            foreach (string roleName in roleNames)
            {
                if (!context.Roles.Any(r => r.Name == roleName))
                {
                    IdentityRole role = new IdentityRole {Name = roleName};
                    IdentityResult result = roleManager.Create(role);
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join("\n", result.Errors));
                    }
                }
            }
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);
            string[] emails = new[] {"teacher@teacher.se" };
            string[] fullName = new[] {"Läraren Lärarsson"};
            int i = 0;
            foreach (string email in emails)
            {
                if (!context.Users.Any(u => u.UserName == email))
                {
                    ApplicationUser user =
                        new ApplicationUser {UserName = email, Email = email, FullName = fullName[i]};
                    var result = userManager.Create(user, "password");
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join("\n", result.Errors));
                    }
                }
                i++;

            }
            ApplicationUser adminUser = userManager.FindByName("teacher@teacher.se");
            userManager.AddToRole(adminUser.Id, "Teacher");
            
            

            CourseModel course = new CourseModel{Name = "kursnamn", Description = "beskrivning", StartDate = DateTime.Now, Moduls = new List<ModulModel>()};
            
            ModulModel modulModel = new ModulModel{Name = "modulnamn", Description = "beskrivning", StartDate = DateTime.Now, EndDate = DateTime.Now };
            
            course.Moduls.Add(modulModel);
            
            context.Courses.Add(course);
            //adminUser = userManager.FindByName("admin@Gymbokning.se");
            //userManager.AddToRole(adminUser.Id, "Admin");

        }
    }
}

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

            string[] roleNames = new[] { "Teacher", "Student" };
            foreach (string roleName in roleNames)
            {
                if (!context.Roles.Any(r => r.Name == roleName))
                {
                    IdentityRole role = new IdentityRole { Name = roleName };
                    IdentityResult result = roleManager.Create(role);
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join("\n", result.Errors));
                    }
                }
            }
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);
            string[] emails = new[] { "teacher@teacher.se" };
            string[] fullName = new[] { "Läraren Lärarsson" };
            int i = 0;
            foreach (string email in emails)
            {
                if (!context.Users.Any(u => u.UserName == email))
                {
                    ApplicationUser user =
                        new ApplicationUser { UserName = email, Email = email, FullName = fullName[i] };
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



            CourseModel course = new CourseModel { Name = "kursnamn", Description = "beskrivning", StartDate = DateTime.Now, Moduls = new List<ModulModel>() };

            ModulModel modulModel = new ModulModel { Name = "modulnamn", Description = "beskrivning", StartDate = DateTime.Now, EndDate = DateTime.Now };

            course.Moduls.Add(modulModel);

            context.Courses.AddOrUpdate(p => p.Name, course);

            course = new CourseModel { Name = "Python", Description = "Grundläggande python", StartDate = DateTime.Now, Moduls = new List<ModulModel>(), Students = new List<ApplicationUser>() };

            modulModel = new ModulModel { Name = "Dokumentation", Description = "Skapa dokument", StartDate = DateTime.Now, EndDate = DateTime.Now, Activities = new List<ActivityModel>() };

            ActivityModel activity = new ActivityModel
            {
                Description = "Skriva god dokumentation",
                Submission = false,
                Name = "Dokumentation",
                TypeOfActivity = "E-learing",
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(3),
                Modul = modulModel,
                Assignments = new List<AssignmentModel>()
            };

            modulModel.Activities.Add(activity);

            activity = new ActivityModel
            {
                Description = "Hur skriver vi god dokumentation",
                Submission = true,
                Name = "Dokumentation",
                TypeOfActivity = "Föreläsning",
                StartDate = DateTime.Now.AddDays(4),
                EndDate = DateTime.Now.AddDays(5),
                Modul = modulModel,
                Assignments = new List<AssignmentModel>()
            };

            modulModel.Activities.Add(activity);

            activity = new ActivityModel
            {
                Description = "Grundläggande objektorientering",
                Submission = false,
                Name = "Objektorienterad programmering",
                TypeOfActivity = "Föreläsning",
                StartDate = DateTime.Now.AddDays(4),
                EndDate = DateTime.Now.AddDays(5).AddHours(5),
                Modul = modulModel,
                Assignments = new List<AssignmentModel>()
            };

            modulModel.Activities.Add(activity);

            course.Moduls.Add(modulModel);

            ApplicationUser student = new ApplicationUser
            {
                UserName = "LenaK@google.com",
                Email = "LenaK@google.com",
                FullName = "Lena Karlsson",
                CourseId = course.Id
            };
            if (!context.Users.Where(g => g.UserName == student.UserName).Any())
            {
                userManager.Create(student, "password");
                userManager.AddToRole(student.Id, "Student");
                course.Students.Add(student);
            }

            context.Courses.AddOrUpdate(p => p.Name, course);

            course = new CourseModel { Name = ".NET", Description = "C#, HTML, CSS, Javascript, jQuery, MVC", StartDate = DateTime.Now, Moduls = new List<ModulModel>(), Students = new List<ApplicationUser>() };

            modulModel = new ModulModel { Name = "C#", Description = "God grund att stå på för framtiden", StartDate = DateTime.Now, EndDate = DateTime.Now, Activities = new List<ActivityModel>() };

            activity = new ActivityModel
            {
                Description = "Skriva god dokumentation",
                Submission = false,
                Name = "Dokumentation",
                TypeOfActivity = "E-learing",
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(3),
                Modul = modulModel,
                Assignments = new List<AssignmentModel>()
            };

            modulModel.Activities.Add(activity);

            activity = new ActivityModel
            {
                Description = "Hur skriver vi god dokumentation",
                Submission = true,
                Name = "Dokumentation",
                TypeOfActivity = "Föreläsning",
                StartDate = DateTime.Now.AddDays(4),
                EndDate = DateTime.Now.AddDays(5),
                Modul = modulModel,
                Assignments = new List<AssignmentModel>()
            };

            modulModel.Activities.Add(activity);

            activity = new ActivityModel
            {
                Description = "Grundläggande objektorientering",
                Submission = false,
                Name = "Objektorienterad programmering",
                TypeOfActivity = "Föreläsning",
                StartDate = DateTime.Now.AddDays(4),
                EndDate = DateTime.Now.AddDays(5).AddHours(5),
                Modul = modulModel,
                Assignments = new List<AssignmentModel>()
            };

            modulModel.Activities.Add(activity);

            course.Moduls.Add(modulModel);

            student = new ApplicationUser
            {
                UserName = "LenaP@google.com",
                Email = "LenaP@google.com",
                FullName = "Lena Karlsson",
                CourseId = course.Id
            };
            if (!context.Users.Where(g => g.UserName == student.UserName).Any())
            {
                userManager.Create(student, "password");
                userManager.AddToRole(student.Id, "Student");
                course.Students.Add(student);
            }

            context.Courses.AddOrUpdate(p => p.Name, course);

            //adminUser = userManager.FindByName("admin@Gymbokning.se");
            //userManager.AddToRole(adminUser.Id, "Admin");

        }
    }
}

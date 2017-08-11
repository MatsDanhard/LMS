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

            CourseModel course = new CourseModel
            {
                Name = "Lorem",
                Description = "beskrivning",
                Moduls = new List<ModulModel>(),
                StartDate = new DateTime(2017,7,3,8,0,0)
            };

            ModulModel modulModel = new ModulModel
            {
                Name = "Ipsum",
                Description = "Lorem ipsum dolor sit amet, et sed orci, turpis suscipit in duis porttitor amet eget, varius dolor, facilisi convallis quis massa. Elementum praesentium dui cursus, metus in ipsum orci mollis justo sit, ac libero mauris erat. Turpis porttitor mauris nibh, vehicula quis etiam, hendrerit elementum duis ultricies. Tempus nulla amet luctus venenatis, vulputate praesent tortor ac. Mauris eu in, convallis venenatis, a sociis sit orci lobortis quam maecenas, convallis mattis ac at sodales. Libero eleifend nibh at, sit enim ultrices cras, in aspernatur arcu. Ultricies hendrerit pellentesque, phasellus suspendisse vel mollis sociosqu, lacus leo metus, ut omnis et sapien donec sit, dis sociis enim quisque id sapien iaculis. Vitae aenean sit massa suspendisse, sit suscipit eu lorem, tellus massa dictum donec velit orci. Ante aliquam auctor, metus fermentum a eleifend ligula ut donec, leo suspendisse lacus donec donec, placerat ligula molestie amet dui, molestie id mi lectus tristique interdum. Montes ut, sed ipsum et massa. Quis justo laboriosam lorem rhoncus amet, luctus cras in, eu proin eget hendrerit at sem id, integer id purus nulla netus mi. Vero volutpat eu suspendisse, id at arcu magna etiam.",
                StartDate = new DateTime(2017,7,3,8,0,0),
                EndDate = new DateTime(2017,8,18,17,0,0)
            };


            course.Moduls.Add(modulModel);

            context.Courses.AddOrUpdate(p => p.Name, course);

            course = new CourseModel
            {
                Name = "Python",
                Description = "Python is a widely used high-level programming language for general-purpose programming, created by Guido van Rossum and first released in 1991. An interpreted language, Python has a design philosophy which emphasizes code readability (notably using whitespace indentation to delimit code blocks rather than curly brackets or keywords), and a syntax which allows programmers to express concepts in fewer lines of code than might be used in languages such as C++ or Java.[22][23] The language provides constructs intended to enable writing clear programs on both a small and large scale.[24] Python features a dynamic type system and automatic memory management and supports multiple programming paradigms, including object - oriented, imperative, functional programming, and procedural styles.It has a large and comprehensive standard library.[25] Python interpreters are available for many operating systems, allowing Python code to run on a wide variety of systems.CPython, the reference implementation of Python, is open source software[26] and has a community - based development model, as do nearly all of its variant implementations.CPython is managed by the non-profit Python Software Foundation.",
                Moduls = new List<ModulModel>(),
                Students = new List<ApplicationUser>(),
                StartDate = new DateTime(2017,7,3,8,0,0)
            };

            modulModel = new ModulModel { Name = "Dokumentation", Description = "Skapa dokument", Activities = new List<ActivityModel>(), StartDate = new DateTime(2017, 7, 4, 8, 0, 0), EndDate = new DateTime(2017, 9, 15, 17, 0, 0) };

            ActivityModel activity = new ActivityModel
            {
                Description = "Skriva god dokumentation",
                Submission = false,
                Name = "Dokumentation",
                TypeOfActivity = "E-learing",
                StartDate = new DateTime(2017,7,4,8,0,0),
                EndDate = new DateTime(2017, 7, 4, 17, 0, 0),
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
                StartDate = new DateTime(2017, 7, 10, 8, 0, 0),
                EndDate = new DateTime(2017, 7, 10, 17, 0, 0),
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
                StartDate = new DateTime(2017, 7, 17, 8, 0, 0),
                EndDate = new DateTime(2017, 7, 17, 17, 0, 0),
                Modul = modulModel,
                Assignments = new List<AssignmentModel>()
            };

            modulModel.Activities.Add(activity);

            course.Moduls.Add(modulModel);

            context.Courses.AddOrUpdate(p => p.Name, course);

            context.SaveChanges();

            course = new CourseModel { Name = ".NET", Description = "C#, HTML, CSS, Javascript, jQuery, MVC", Moduls = new List<ModulModel>(), Students = new List<ApplicationUser>(), StartDate = new DateTime(2017,7,10,8,0,0) };

            modulModel = new ModulModel { Name = "C# bas", Description = "God grund att stå på för framtiden", Activities = new List<ActivityModel>(), StartDate = new DateTime(2017,7,10,8,0,0), EndDate = new DateTime(2017,7,14,17,0,0) };

            activity = new ActivityModel
            {
                Description = "Skriva god dokumentation",
                Submission = false,
                Name = "Dokumentation",
                TypeOfActivity = "E-learing",
                StartDate = new DateTime(2017,7,10,8,0,0),
                EndDate = new DateTime(2017, 7, 10, 17, 0, 0),
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
                StartDate = new DateTime(2017,7,11,8,0,0),
                EndDate = new DateTime(2017, 7, 11, 17, 0, 0),
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
                StartDate = new DateTime(2017,7,13,8,0,0),
                EndDate = new DateTime(2017, 7, 13, 17, 0, 0),
                Modul = modulModel,
                Assignments = new List<AssignmentModel>()
            };

            modulModel.Activities.Add(activity);

            course.Moduls.Add(modulModel);

            modulModel = new ModulModel { Name = "C# fördjupning", Description = "Mer avancerad kod", Activities = new List<ActivityModel>(), StartDate = new DateTime(2017,7,31,8,0,0), EndDate = new DateTime(2017,8,11,17,0,0) };

            activity = new ActivityModel
            {
                Description = "Skriva god dokumentation",
                Submission = false,
                Name = "Dokumentation",
                TypeOfActivity = "E-learing",
                StartDate = new DateTime(2017,7,31,8,0,0),
                EndDate = new DateTime(2017, 7, 31, 17, 0, 0),
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
                StartDate = new DateTime(2017, 8, 1, 8, 0, 0),
                EndDate = new DateTime(2017, 8, 1, 17, 0, 0),
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
                StartDate = new DateTime(2017, 8, 3, 8, 0, 0),
                EndDate = new DateTime(2017, 8, 3, 17, 0, 0),
                Modul = modulModel,
                Assignments = new List<AssignmentModel>()
            };

            modulModel.Activities.Add(activity);

            course.Moduls.Add(modulModel);

            context.Courses.AddOrUpdate(p => p.Name, course);

            // Ny kurs
            context.Courses.AddOrUpdate(p => p.Name,
                new CourseModel
                {
                    Name = ".NET fortsättning",
                    Description = "C#, HTML, CSS, Javascript, jQuery, MVC",
                    StartDate = new DateTime(2017, 9, 4, 8, 0, 0),
                    Moduls = new List<ModulModel> {
                        new ModulModel {
                            Name = "C# bas",
                            Description = "God grund att stå på för framtiden",
                            StartDate = new DateTime(2017, 9, 4, 8, 0, 0),
                            EndDate = new DateTime(2017, 10, 6, 17, 0, 0),
                            Activities = new List<ActivityModel>
                            {
                                new ActivityModel
                                {
                                    Description = "Grundläggande objektorientering",
                                    Submission = false,
                                    Name = "Objektorienterad programmering",
                                    TypeOfActivity = "Föreläsning",
                                    Assignments = new List<AssignmentModel>(),
                                    StartDate = new DateTime(2017, 9, 4, 8, 0, 0),
                                    EndDate = new DateTime(2017, 9, 4, 17, 0, 0)
                                },
                                new ActivityModel
                                {
                                    Description = "Avancerad objektorientering",
                                    Submission = false,
                                    Name = "Objektorienterad programmering",
                                    TypeOfActivity = "Föreläsning",
                                    Assignments = new List<AssignmentModel>(),
                                    StartDate = new DateTime(2017, 9, 7, 8, 0, 0),
                                    EndDate = new DateTime(2017, 9, 7, 17, 0, 0)
                                }
                            }
                        },
                        new ModulModel
                        {
                            Name = "C# fördjupning",
                            Description = "Fördjupning",
                            StartDate = new DateTime(2017, 10, 9, 8, 0, 0),
                            EndDate = new DateTime(2017, 10, 20, 17, 0, 0),
                            Activities = new List<ActivityModel>
                            {
                                new ActivityModel
                                {
                                    Description = "Grundläggande objektorientering",
                                    Submission = false,
                                    Name = "Objektorienterad programmering",
                                    TypeOfActivity = "Föreläsning",
                                    StartDate = new DateTime(2017, 10, 9, 8, 0, 0),
                                    EndDate = new DateTime(2017, 10, 9, 17, 0, 0),
                                    Assignments = new List<AssignmentModel>()
                                },
                                new ActivityModel
                                {
                                    Description = "Avancerad objektorientering",
                                    Submission = false,
                                    Name = "Objektorienterad programmering",
                                    TypeOfActivity = "Föreläsning",
                                    StartDate = new DateTime(2017, 10, 12, 8, 0, 0),
                                    EndDate = new DateTime(2017, 10, 12, 17, 0, 0),
                                    Assignments = new List<AssignmentModel>()
                                }
                            }
                        }
                    },
                    Students = new List<ApplicationUser>()
                });


            context.SaveChanges();

            context.Courses.AddOrUpdate(p => p.Name,
                new CourseModel
                {
                    Name = "Erlang för ingenjörer",
                    Description = "Det unika med programmeringsspråket Erlang är möjligheten att skapa applikationer som effektivt hanterar ett mycket stort antal samtidiga processer, utan att vare sig en själv eller servern behöver anstränga sig. Utbildningen innehåller ett stort antal mindre övningsuppgifter som på ett pedagogiskt sätt stegvis bygger upp kunnandet och insikterna i språket Erlang. Denna utbildning riktar sig till er som planerar att ta in Erlang som utvecklingsspråk och snabbt vill komma in och bli effektiva med best practices",
                    StartDate = new DateTime(2017, 6, 5, 8, 0, 0),
                    Moduls = new List<ModulModel> {
                        new ModulModel {
                            Name = "Erlang grunder",
                            Description = "God grund att stå på för framtiden",
                            StartDate = new DateTime(2017, 6, 5, 8, 0, 0),
                            EndDate = new DateTime(2017,6,28,17,0,0),
                            Activities = new List<ActivityModel>
                            {
                                new ActivityModel
                                {
                                    Description = "Introduktion",
                                    Submission = false,
                                    Name = "Introduktion",
                                    TypeOfActivity = "Föreläsning",
                                    StartDate = new DateTime(2017,6,5,8,0,0),
                                    EndDate = new DateTime(2017,6,5,17,0,0),
                                    Assignments = new List<AssignmentModel>()
                                },
                                new ActivityModel
                                {
                                    Description = "Pluralsight",
                                    Submission = false,
                                    Name = "Intro till Erlang",
                                    TypeOfActivity = "E-Learning",
                                    StartDate = new DateTime(2017,6,25,8,0,0),
                                    EndDate = new DateTime(2017,6,25,17,0,0),
                                    Assignments = new List<AssignmentModel>()
                                }
                            }
                        },
                        new ModulModel
                        {
                            Name = "Integrering",
                            Description = "Hur kopplar man Erlang till andra miljöer",
                            StartDate = new DateTime(2017,7,1,8,0,0),
                            EndDate = new DateTime(2017,7,19,17,0,0),
                            Activities = new List<ActivityModel>
                            {
                                new ActivityModel
                                {
                                    Description = "Koppla till mvc",
                                    Submission = false,
                                    Name = "MVC",
                                    TypeOfActivity = "Föreläsning",
                                    StartDate = new DateTime(2017,7,3,8,0,0),
                                    EndDate = new DateTime(2017,7,3,17,0,0),
                                    Assignments = new List<AssignmentModel>()
                                },
                                new ActivityModel
                                {
                                    Description = "Pluralsight",
                                    Submission = false,
                                    Name = "MVC med Erlang",
                                    TypeOfActivity = "E-Learning",
                                    StartDate = new DateTime(2017,7,18,8,0,0),
                                    EndDate = new DateTime(2017,7,18,17,0,0),
                                    Assignments = new List<AssignmentModel>()
                                }
                            }
                        }
                    },
                    Students = new List<ApplicationUser>()
                });

            context.SaveChanges();

            // Lägger till studenter
            course = context.Courses.Where(g => g.Name == "Python").First();

            ApplicationUser student;
            emails = new[] { "KlaraPersson@google.se", "Tussilago@live.se", "Peter23@outlook.com", "LenaEriksson@google.com", "LenaP@google.com", "Frans@yahoo.com", "Erik@live.se", "Fridahemma@live.se" };
            fullName = new[] { "Klara Persson", "Petra Eriksson", "Peter Johnsson", "Lena Eriksson", "Lena Eriksson", "Fredrik Karlsson", "Erik Eriksson", "Frida Ljung" };
            i = 0;
            foreach (string email in emails)
            {
                if (!context.Users.Any(u => u.UserName == email))
                {
                    student = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        FullName = fullName[i],
                        CourseId = course.Id
                    };
                    var result = userManager.Create(student, "password");
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join("\n", result.Errors));
                    }
                    userManager.AddToRole(student.Id, "Student");
                    course.Students.Add(student);
                }
                i++;
            }

            course = context.Courses.Where(g => g.Name == ".Net").First();

            emails = new[] { "ErikEriksson@google.se", "Smultron89@live.se", "Peter2012@outlook.com" };
            fullName = new[] { "Erik Eriksson", "Jan Svensson", "Peter Johnsson" };
            i = 0;
            foreach (string email in emails)
            {
                if (!context.Users.Any(u => u.UserName == email))
                {
                    student = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        FullName = fullName[i],
                        CourseId = course.Id
                    };
                    var result = userManager.Create(student, "password");
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join("\n", result.Errors));
                    }
                    userManager.AddToRole(student.Id, "Student");
                    course.Students.Add(student);
                }
                i++;
            }

            course = context.Courses.Where(g => g.Name == "Lorem").First();

            emails = new[] { "ErikEriksson1@google.se", "Smultron891@live.se", "Peter20121@outlook.com" };
            fullName = new[] { "Erik Eriksson", "Jan Svensson", "Peter Johnsson" };
            i = 0;
            foreach (string email in emails)
            {
                if (!context.Users.Any(u => u.UserName == email))
                {
                    student = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        FullName = fullName[i],
                        CourseId = course.Id
                    };
                    var result = userManager.Create(student, "password");
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join("\n", result.Errors));
                    }
                    userManager.AddToRole(student.Id, "Student");
                    course.Students.Add(student);
                }
                i++;
            }

            course = context.Courses.Where(g => g.Name == ".NET fortsättning").First();

            emails = new[] { "ErikEriksson11@google.se", "Smultron8911@live.se", "Peter201211@outlook.com" };
            fullName = new[] { "Erik Eriksson", "Jan Svensson", "Peter Johnsson" };
            i = 0;
            foreach (string email in emails)
            {
                if (!context.Users.Any(u => u.UserName == email))
                {
                    student = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        FullName = fullName[i],
                        CourseId = course.Id
                    };
                    var result = userManager.Create(student, "password");
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join("\n", result.Errors));
                    }
                    userManager.AddToRole(student.Id, "Student");
                    course.Students.Add(student);
                }
                i++;
            }

            course = context.Courses.Where(g => g.Name == "Erlang för ingenjörer").First();

            //emails = new[] { "ErikEriksson89@google.se", "Smultronvagen@live.se", "PeterJohn@outlook.com" };
            //fullName = new[] { "Erik Eriksson", "Anna Svensson", "Peter Johannesson" };
            //i = 0;
            //foreach (string email in emails)
            //{
            //    if (!context.Users.Any(u => u.UserName == email))
            //    {
            //        student = new ApplicationUser
            //        {
            //            UserName = email,
            //            Email = email,
            //            FullName = fullName[i],
            //            CourseId = course.Id
            //        };
            //        var result = userManager.Create(student, "password");
            //        if (!result.Succeeded)
            //        {
            //            throw new Exception(string.Join("\n", result.Errors));
            //        }
            //        userManager.AddToRole(student.Id, "Student");
            //        course.Students.Add(student);
            //    }
            //    i++;
            //}

            for (int p = 1; p < 100; p++)
            {
                student = new ApplicationUser
                {
                    UserName = "Erlang" + p + "@outlook.com",
                    Email = "Erlang" + p + "@outlook.com",
                    FullName = "Erlang student",
                    CourseId = course.Id
                };
                var result = userManager.Create(student, "password");
                userManager.AddToRole(student.Id, "Student");
                course.Students.Add(student);
            }

            //adminUser = userManager.FindByName("admin@Gymbokning.se");
            //userManager.AddToRole(adminUser.Id, "Admin");

        }
    }
}

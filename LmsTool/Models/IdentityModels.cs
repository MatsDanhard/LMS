using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using LmsTool.Models.DbModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LmsTool.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public int? CourseId { get; set; }
        public virtual CourseModel Course{ get; set; }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<ModulModel> Moduls { get; set; }
        public DbSet<ActivityModel> Activities { get; set; }
        public DbSet<AssignmentModel> Assignments { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<LmsTool.Models.Viewmodels.ViewStudents> ViewStudents { get; set; }

        public System.Data.Entity.DbSet<LmsTool.Models.RegisterViewModel> RegisterViewModels { get; set; }

        public System.Data.Entity.DbSet<LmsTool.Models.Viewmodels.ViewModuls> ViewModuls { get; set; }

        public System.Data.Entity.DbSet<LmsTool.Models.RegisterViewModelTeacher> RegisterViewModelTeachers { get; set; }


        //public System.Data.Entity.DbSet<LmsTool.Models.ApplicationUser> ApplicationUsers { get; set; }


    }
}
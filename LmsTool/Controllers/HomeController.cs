using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LmsTool.Models;
using LmsTool.Models.DbModels;
using LmsTool.Models.Viewmodels;
using System.IO;

namespace LmsTool.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class HomeController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index()
        {

            var courses = _db.Courses.Include(m => m.Moduls)
                .Include(s => s.Students)
                .OrderBy(d => d.StartDate)
                .ToList();


            List<ViewCourses> model = new List<ViewCourses>();

            

            foreach (var course in courses)
            {
                model.Add(new ViewCourses
                {
                    Name = course.Name,
                    Description = course.Description,
                    Id = course.Id,
                    NrOfModuls = course.Moduls.Count,
                    NrOfStudents = course.Students.Count,
                    StartDate = course.StartDate,
                    Document = course.Document
                });
            }





            return View(model);
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public FileResult Download(string FileName)
        {
            var FileVirtualPath = "~/Documents/" + FileName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }
    }
}
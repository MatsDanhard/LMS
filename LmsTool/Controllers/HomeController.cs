using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LmsTool.Models;
using LmsTool.Models.DbModels;

namespace LmsTool.Controllers
{
    
    public class HomeController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index()
        {

            List<CourseModel> model = _db.Courses.Include(m => m.Moduls).ToList();


            return View(model);
        }
        [HttpPost]
        public ActionResult Index(CourseModel model)
        {
            return Content("Thanks", "text/html");
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
    }
}
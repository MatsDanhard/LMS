using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LmsTool.Models;
using LmsTool.Models.DbModels;
using LmsTool.Models.StudentViewmodels;
using LmsTool.Models.Viewmodels;
using Microsoft.AspNet.Identity;

namespace LmsTool.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Student
        public ActionResult Index()
        {

            DateTime start = DateTime.Today;
            DateTime end = DateTime.Today.AddHours(23).AddMinutes(59);

            var student = db.Users.Find(User.Identity.GetUserId());
            var course = db.Courses.Find(student.CourseId);
            var activities = db.Activities.Where(a => a.Modul.Course.Id == course.Id && a.StartDate > start && a.StartDate < end).ToList();
            var assignments = db.Assignments.Include(a => a.Activity).Where(u => u.UserId == student.Id).OrderByDescending(o => o.Deadline).ToList();
            var modul = db.Moduls.Include(a => a.Activities).Where(m => m.CourseId == course.Id).OrderBy(o => o.StartDate) .ToList();

        

            index studentIndex = new index{Assignments = assignments, Course = course, Moduls = modul, TodaysActivities = activities, StudentName = student.FullName, TodaysDate = DateTime.Today.ToString()};

            return View("~/Views/Home/IndexStudent.cshtml", studentIndex);
        }

        
        public ActionResult InfoModul(int id)
        {



            ModulModel modul = db.Moduls.Find(id);
            
            return PartialView(modul);
        }

        public ActionResult InfoActivity(int id)
        {

            ActivityModel activity = db.Activities.Find(id);
            
            return View(activity);
        }

        public ActionResult InfoAssignment(int id)
        {
           
            ViewStudents viewStudents = db.ViewStudents.Find(id);
            if (viewStudents == null)
            {
                return HttpNotFound();
            }
            return View(viewStudents);
        }



        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,Email")] ViewStudents viewStudents)
        {
            if (ModelState.IsValid)
            {
                db.ViewStudents.Add(viewStudents);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewStudents);
        }

        // GET: Student/Edit/5
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

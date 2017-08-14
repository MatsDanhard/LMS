using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LmsTool.Models;
using LmsTool.Models.DbModels;
using LmsTool.Models.StudentViewmodels;
using LmsTool.Models.Viewmodels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System.IO;

namespace LmsTool.Controllers
{
    [Authorize(Roles="Student")]
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
            var assignments = db.Assignments.Include(a => a.Activity).Where(u => u.UserId == student.Id).OrderBy(o => o.Deadline).ToList();
            var modul = db.Moduls.Include(a => a.Activities).Where(m => m.CourseId == course.Id).OrderBy(o => o.StartDate).ToList();

         

            index studentIndex = new index { Assignments = assignments, Course = course, Moduls = modul, TodaysActivities = activities, StudentName = student.FullName, TodaysDate = DateTime.Today.ToString() };

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
            ModulModel modul = db.Moduls.Find(activity.ModulId);
            activity.Modul = modul;
            return PartialView(activity);
        }

        public ActionResult InfoAssignment(int id)
        {

            AssignmentModel assignment = db.Assignments.Find(id);

            return PartialView(assignment);
        }



        // GET: Student/Create


        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InfoAssignment([Bind(Include = "Id,Document")] AssignmentModel assignmentModel)
        {
            if (assignmentModel.Document.IsNullOrWhiteSpace())
            {
                return RedirectToAction("Index", "Student");
            }

            AssignmentModel upd = db.Assignments.Find(assignmentModel.Id);

            upd.Document = assignmentModel.Document;
            upd.Submitted = DateTime.Now;


            db.Assignments.AddOrUpdate(upd);
            db.SaveChanges();
            return RedirectToAction("Index", "Student");


        }

        public ActionResult RedoAssignment(int id)
        {
            
            AssignmentModel assignment = db.Assignments.Find(id);
            assignment.Document = null;
            assignment.Submitted = null;
            
            assignment.Redo = false;
            db.Entry(assignment).State = EntityState.Modified;
            db.SaveChanges();

           return RedirectToAction("Index");
        }

        public JsonResult GetActivities()
        {

            var student = db.Users.Find(User.Identity.GetUserId());
            var course = db.Courses.Find(student.CourseId);
            //var modul = db.Moduls.Where(m => m.CourseId == course.Id);
            var activities = db.Activities.Where(a => a.Modul.CourseId == course.Id)
                .ToList()
                .Select(item => new 
                {
                    id = item.Id,
                    title = item.Name,
                    start = item.StartDate,
                    end = item.EndDate
                    

                })
                .ToArray();
            

            

            return Json(activities, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StudentsInClass()
        {
            var myself = db.Users.Find(User.Identity.GetUserId()).CourseId;
           

            var Students = db.Users.Where(model => model.CourseId == myself)
                .OrderBy(n => n.FullName)
                .ToList();

            List<ViewStudents> listStudents = new List<ViewStudents>();

            foreach (var user in Students)
            {
                listStudents.Add(new ViewStudents { Id = user.Id, Email = user.Email, FullName = user.FullName, Assignments = db.Assignments.Where(a => a.UserId == user.Email).ToList() });
            }

            return View("~/Views/Course/Index.cshtml", listStudents);
        }

        public ActionResult ViewActivities(int id)
        {
            var myself = db.Users.Find(User.Identity.GetUserId()).CourseId;
            var modul = db.Moduls.Find(id);
            if (modul.CourseId == myself)
            {

                var activities = db.Activities.Include(a => a.Assignments).Where(a => a.ModulId == id).OrderBy(d => d.StartDate);

                List<ViewActivitys> model = new List<ViewActivitys>();
                if (activities.Any())
                {

                    foreach (var activity in activities)
                    {
                        model.Add(new ViewActivitys
                        {
                            TypeOfActivity = activity.TypeOfActivity,
                            Assignments = activity.Assignments.ToList(),
                            Description = activity.Description,
                            EndDate = activity.EndDate,
                            Id = activity.Id,
                            ModulId = activity.ModulId,
                            Name = activity.Name,
                            StartDate = activity.StartDate,
                            ModulName = modul.Name,
                            ModulStartStr = modul.StartDate.ToShortDateString(),
                            ModulEndStr = modul.EndDate.ToShortDateString(),
                            Document = activity.Document

                        });
                    }
                }

                return View("~/Views/Activity/Index.cshtml", model);
            }
            else
            {
                return View("~/Views/Activity/Index.cshtml");
            }


            
        }
        public FileResult Download(string FileName)
        {
            var FileVirtualPath = "~/Documents/" + FileName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }

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

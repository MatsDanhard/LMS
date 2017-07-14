using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using LmsTool.Models;
using LmsTool.Models.DbModels;
using LmsTool.Models.Viewmodels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LmsTool.Controllers
{
    public class CourseController : Controller
    {
       
        private ApplicationDbContext db = new ApplicationDbContext();
       

        // GET: Course
        public ActionResult Index(int? id)
        {
            ViewBag.currentModul = id;
            var Students = db.Users.Include(model => model.Assignments).Where(model => model.CourseId == id).ToList();

            List<ViewStudents> listStudents = new List<ViewStudents>();

            foreach (var user in Students)
            {
                listStudents.Add( new ViewStudents{Id = user.Id,Email = user.Email, FullName = user.FullName, Assignments = user.Assignments.ToList()});
            }

            return PartialView(listStudents);
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseModel courseModel = db.Courses.Find(id);
            if (courseModel == null)
            {
                return HttpNotFound();
            }
            return View(courseModel);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            CourseModel model = new CourseModel();

            return PartialView(model);
            
            
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,StartDate")] CourseModel courseModel)
        {
            if (ModelState.IsValid)
            {
                CourseModel course = new CourseModel
                {
                    Id = courseModel.Id, Description = courseModel.Description, Name = courseModel.Name, StartDate = Convert.ToDateTime(courseModel.StartDate)
                };


                db.Courses.Add(courseModel);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View(courseModel);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseModel courseModel = db.Courses.Find(id);
            if (courseModel == null)
            {
                return HttpNotFound();
            }
            return PartialView(courseModel);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StartDate")] CourseModel courseModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(courseModel);
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseModel courseModel = db.Courses.Find(id);
            if (courseModel == null)
            {
                return HttpNotFound();
            }
            return PartialView(courseModel);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseModel courseModel = db.Courses.Find(id);
            UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(db);
            ApplicationUserManager manager = new ApplicationUserManager(store);
            var query = manager.Users.Where(u => u.CourseId == id).ToList();
            
            
            

            foreach (var user in query)
            {
               var delete =  db.Users.Find(user.Id);
                manager.Delete(delete);
            }

            db.Courses.Remove(courseModel);
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        //public ActionResult AddStudent(int id)
        //{



        //    AddStudent model = new AddStudent{CourseId = id};


        //    return PartialView(model);


        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddStudent([Bind(Include = "CourseId,FullName,Email")] AddStudent model)
        //{
        //    UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
        //    UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

        //    var query = db.ApplicationUsers.ToList().FirstOrDefault(u => u.Email == model.Email);

        //    if (query != null)
        //    {
        //        return RedirectToAction("AddStudent", model);
        //    }

            
        //    return View("");
        //}

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

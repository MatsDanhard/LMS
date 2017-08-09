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
using System.IO;

namespace LmsTool.Controllers
{
    public class CourseController : Controller
    {
       
        private ApplicationDbContext db = new ApplicationDbContext();
       

        // GET: Course
        public ActionResult Index(int? id) // För elevlistan
        {
            ViewBag.CurrentCourse = id;
            var Students = db.Users.Where(model => model.CourseId == id)
                .OrderBy(n => n.FullName)
                .ToList();

            List<ViewStudents> listStudents = new List<ViewStudents>();

            foreach (var user in Students)
            {
                listStudents.Add( new ViewStudents{Id = user.Id,Email = user.Email, FullName = user.FullName, Assignments = db.Assignments.Where(a => a.UserId == user.Email).ToList()});
            }

            return View(listStudents);
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

        public ActionResult Create([Bind(Include = "Id,Name,Description,StartDate,file")] CourseModel courseModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                if (file != null && file.ContentLength > 0)
                {
                    string path = Path.Combine(Server.MapPath("~/Documents"), Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    courseModel.Document = file.FileName;
                }


                db.Courses.Add(courseModel);
                db.SaveChanges();
                //TempData["postResult"] = "Saved!";
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("",@"Kunde ej spara kursen");
            
             return PartialView("Create",courseModel); 
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
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StartDate,Document")] CourseModel courseModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    string path = Path.Combine(Server.MapPath("~/Documents"), Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    courseModel.Document = file.FileName;
                }

                db.Entry(courseModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return PartialView(courseModel);
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

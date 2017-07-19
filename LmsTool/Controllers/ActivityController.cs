using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using LmsTool.Models;
using LmsTool.Models.DbModels;
using LmsTool.Models.Viewmodels;

namespace LmsTool.Controllers
{
    public class ActivityController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        

        // GET: Activity
        public ActionResult Index(int? id)
        {

            ViewBag.currentModul = id;


            var modul = db.Moduls.Find(id);
            var activities = db.Activities.Include(a => a.Assignments).Where(a => a.ModulId == id);

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
                        ModulEndStr = modul.EndDate.ToShortDateString()

                    });
                }
            }
            else
            {
                model.Add(new ViewActivitys{
                    ModulName = modul.Name,
                    ModulStartStr = modul.StartDate.ToShortDateString(),
                    ModulEndStr = modul.EndDate.ToShortDateString()
                });
            }


            //ViewBag.ModulName = modul.Name;
                //ViewBag.ModulStart = modul.StartDate.ToShortDateString();
                //ViewBag.ModulEnd = modul.EndDate.ToShortDateString();

            

            return View(model);
        }

        // GET: Activity
        public ActionResult IndexAssignment(int id)
        {
            var query = db.Assignments.Where(a => a.ActivityId == id);
          




            return View(query.ToList());
        }

        // GET: Activity/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityModel activityModel = db.Activities.Find(id);
            if (activityModel == null)
            {
                return HttpNotFound();
            }
            return View(activityModel);
        }


        // GET: Activity/CreateAssignment
        public ActionResult CreateAssignment(int id)
        {

            var query = db.Activities.Find(id);

            //var users = db.Users.Where(user => user.Course.Moduls.Contains(query.Modul));
            //List<AssignmentModel> model = new List<AssignmentModel>();
            //foreach (var user in users)
            //{
            //    model.Add(new AssignmentModel{});
            //}

            AssignmentModel model = new AssignmentModel{Activity = query, ActivityId = id};
            


            
           
            return PartialView(model);
        }

        // POST: Activity/CreateAssignment
        [HttpPost]
        public ActionResult CreateAssignment([Bind(Include = "Name,Description,Deadline,ActivityId,Activity")] AssignmentModel assignmentModel)
        {

            
            

            if (ModelState.IsValid)
            {
                var activities = db.Activities.Find(assignmentModel.ActivityId).ModulId;
                var modul = db.Moduls.Find(activities).CourseId;
                var users = db.Users.Where(u => u.Course.Id == modul);
                
                foreach (var user in users)
                {
                    AssignmentModel model = new AssignmentModel
                    {
                        ActivityId = assignmentModel.ActivityId,
                        Activity = assignmentModel.Activity,
                        Deadline = assignmentModel.Deadline,
                        Description = assignmentModel.Description,
                        Name = assignmentModel.Name,
                        UserId = user.UserName


                    };

                    db.Assignments.Add(model);
                    
                }
                db.SaveChanges();

            }





            return RedirectToAction("IndexAssignment","Activity", new {id = assignmentModel.ActivityId});
        }

        // GET: Activity/Create
        public ActionResult Create(int id)
        {

            var query = db.Moduls.Find(id);

            //Todo:1 Partial


            //if (query.Activities.Any())
            //{
            //    var activityStart = query.Activities.OrderBy(a => a.EndDate).Last().EndDate;

            //    CreateActivity model = new CreateActivity{DisplayModulStart = activityStart.ToShortDateString(), ModulId = id, ActivityStart = activityStart, ActivityEnd = activityStart.AddDays(1)};


            //}


            //ViewBag.ModulId = new SelectList(db.Models, "Id", "Name");
            //ActivityModel model = new ActivityModel{ ModulId = id };
            CreateActivity model = new CreateActivity{ ModulId = id, DisplayModulStart = query.StartDate.ToShortDateString(),
                DisplayModulEnd = query.EndDate.ToShortDateString(), ModulName = query.Name, ModulStart = query.StartDate,ModulEnd = query.EndDate};
            return PartialView(model);
        }

        // POST: Activity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TypeOfActivity,Name,Description,Submission,ActivityStart,ActivityEnd,ModulId,ModulStart,ModulEnd")] CreateActivity createActivity)
        {
            



            if (ModelState.IsValid)
            {
               
                {
                    if (createActivity.ActivityStart > createActivity.ModulStart && createActivity.ActivityEnd < createActivity.ModulEnd)
                    {

                       
                        ActivityModel model = new ActivityModel
                        {
                            Name = createActivity.Name,
                            Description = createActivity.Description,
                            StartDate = createActivity.ActivityStart,
                            EndDate = createActivity.ActivityEnd,
                            ModulId = createActivity.ModulId,
                            TypeOfActivity = createActivity.TypeOfActivity,
                            Submission = createActivity.Submission,

                        };

                        db.Activities.Add(model);
                        db.SaveChanges();
                        return model.Submission ? RedirectToAction("CreateAssignment", new {id = model.Id}) : RedirectToAction("Index", "Home");
                    }
                    
                }

                

            }
            ViewBag.dateFailure = "Gick inte att skapa en aktvitet";

            return View(createActivity);
           

            //ViewBag.ModulId = new SelectList(db.Models, "Id", "Name", activityModel.ModulId);

        }

        // GET: Activity/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityModel activityModel = db.Activities.Find(id);
            if (activityModel == null)
            {
                return HttpNotFound();
            }
            
            return PartialView(activityModel);
        }

        // POST: Activity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TypeOfActivity,Name,Description,Submission,StartDate,EndDate,ModulId")] ActivityModel activityModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new {id = activityModel.ModulId});
            }
            
            return View(activityModel);
        }

        // GET: Activity/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityModel activityModel = db.Activities.Find(id);
            if (activityModel == null)
            {
                return HttpNotFound();
            }
            return PartialView(activityModel);
        }

        // POST: Activity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActivityModel activityModel = db.Activities.Find(id);
            db.Activities.Remove(activityModel);
            db.SaveChanges();
            return RedirectToAction("Index", new {id = activityModel.ModulId});
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

﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LmsTool.Models;
using LmsTool.Models.DbModels;
using LmsTool.Models.Viewmodels;

namespace LmsTool.Controllers
{

    public class HomeController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index()
        {

            var courses = _db.Courses.Include(m => m.Moduls).Include(s => s.Students).ToList();


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
                    StartDate = course.StartDate

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
    }
}
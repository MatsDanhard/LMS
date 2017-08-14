using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LmsTool.Models.DbModels;

namespace LmsTool.Models.StudentViewmodels
{
    public class index
    {
        public string StudentName { get; set; }
        public string TodaysDate { get; set; }

        public List<AssignmentModel> Assignments { get; set; }
        public List<ActivityModel> TodaysActivities { get; set; }
        public List<ModulModel> Moduls { get; set; }
        public virtual CourseModel Course { get; set; }
        


    }
}
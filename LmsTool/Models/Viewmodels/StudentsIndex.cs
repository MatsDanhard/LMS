using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LmsTool.Models.DbModels;

namespace LmsTool.Models.Viewmodels
{
    public class StudentsIndex
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string CourseStartDate { get; set; }
        public List<ModulModel> Moduls { get; set; }
    }
}
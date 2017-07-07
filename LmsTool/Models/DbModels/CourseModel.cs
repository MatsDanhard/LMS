using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LmsTool.Models.DbModels
{
    public class CourseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public DateTime StartDate { get; set; } 
        public ICollection<ApplicationUser> Students { get; set; }
        public ICollection<ModulModel> Moduls { get; set; }
    }
}
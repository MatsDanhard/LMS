using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LmsTool.Models.DbModels
{
    public class ModulModel
    {
        public int Id { get; set; }
        [Display(Name = "Modul namn")]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CourseId { get; set; }   
        public CourseModel Course { get; set; }
        public ICollection<ActivityModel> Activities { get; set; }
    }
}
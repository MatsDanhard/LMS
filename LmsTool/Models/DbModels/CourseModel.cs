using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LmsTool.Models.DbModels
{
    public class CourseModel
    {
        public int Id { get; set; }
        [Display(Name = "Kursnamn")]
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;
        public ICollection<ApplicationUser> Students { get; set; }
        public ICollection<ModulModel> Moduls { get; set; }
    }
}
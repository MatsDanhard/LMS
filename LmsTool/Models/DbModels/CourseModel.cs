using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LmsTool.Models.DbModels
{
    public class CourseModel
    {
        public int Id { get; set; }
        [Display(Name = "Kursens namn")]
        public string Name { get; set; }
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }
        [Display(Name = "Startdatum")]
        //[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/mm/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;
        public ICollection<ApplicationUser> Students { get; set; }
        public ICollection<ModulModel> Moduls { get; set; }
    }
}
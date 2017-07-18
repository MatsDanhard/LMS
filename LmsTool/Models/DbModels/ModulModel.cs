using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LmsTool.Models.DbModels
{
    public class ModulModel
    {
        public int Id { get; set; }
        [Display(Name = "Modulens namn")]
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Namnet behöver vara minst tre tecken långt")]
        public string Name { get; set; }
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }
        [Display(Name = "Startdatum")]
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Datumet är inte korrekt angivet")]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [Display(Name = "Slutdatum")]
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Datumet är inte korrekt angivet")]
        public DateTime EndDate { get; set; }= DateTime.Now.AddDays(7);
        public int CourseId { get; set; }   
        public CourseModel Course { get; set; }
        public ICollection<ActivityModel> Activities { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LmsTool.Models.DbModels;

namespace LmsTool.Models.Viewmodels
{
    public class ViewCourses
    {
        public int Id { get; set; }
        [Display(Name = "Kursens namn")]
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Namnet behöver vara minst tre tecken långt")]
        public string Name { get; set; }
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }
        [Display(Name = "Dokument")]
        public string Document { get; set; }
        [Display(Name = "Startdatum")]
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Datumet är inte korrekt angivet")]
        public DateTime StartDate { get; set; } = DateTime.Now;
        public int NrOfStudents { get; set; }
        public int NrOfModuls { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LmsTool.Models.Viewmodels
{
    public class CreateActivity
    {
        //public CreateActivity(string Start, string Stop)
        //{
        //    ms = Start;

        //}

        //private string ms = "";

        [Display(Name = "Aktivitetens namn")]
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Namnet behöver vara minst tre tecken långt")]
        public string Name { get; set; }
        [Display(Name = "Modulens namn")]
        public string ModulName { get; set; }
        [Display(Name = "Typ av aktivitet")]
        public string TypeOfActivity { get; set; }
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }
        [Display(Name = "Startdatum")]
        [DataType(DataType.Date, ErrorMessage = "Datumet är inte korrekt angivet")]
        //[Range(typeof(DateTime), ms, DisplayModulEnd, ErrorMessage ="Datumet måste vara mellan {1} {2}")]
        public DateTime ActivityStart { get; set; } = DateTime.Now;
        [Display(Name = "Slutdatum")]
        public DateTime ActivityEnd { get; set; }
        public DateTime ModulStart { get; set; } 
        public DateTime ModulEnd { get; set; } 
        public string DisplayModulStart { get; set; }
        public string DisplayModulEnd { get; set; }
        public int ModulId { get; set; }
        [Display(Name = "Inlämningsuppgift")]
        public bool Submission { get; set; }
    }
}
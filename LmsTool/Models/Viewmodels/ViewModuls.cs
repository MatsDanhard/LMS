﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LmsTool.Models.DbModels;

namespace LmsTool.Models.Viewmodels
{
    public class ViewModuls
    {
        public int Id { get; set; }
        [Display(Name = "Modulens namn")]
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
        [Display(Name = "Slutdatum")]
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Datumet är inte korrekt angivet")]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(7);
        public int CourseId { get; set; }
       
        public int NrOfActivitys { get; set; }

        public virtual CourseModel Course { get; set; }
    }
}
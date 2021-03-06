﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace LmsTool.Models.DbModels
{
    public class CourseModel
    {
        public int Id { get; set; }
        [Display(Name = "Kursens namn")]
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Namnet behöver vara minst tre tecken långt")]
        public string Name { get; set; }
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }
        [Display(Name = "Startdatum")]
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Datumet är inte korrekt angivet")]
        public DateTime StartDate { get; set; } = DateTime.Now;
        public ICollection<ApplicationUser> Students { get; set; }
        public ICollection<ModulModel> Moduls { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LmsTool.Models.DbModels
{
    public class ActivityModel
    {
        public int Id { get; set; }
        [Display(Name="Typ av aktivitet")]
        public string TypeOfActivity { get; set; }
        [Display(Name = "Aktivitetens namn")]
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Namnet behöver vara minst tre tecken långt")]
        public string Name { get; set; }
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }
        [Display(Name = "Inlämningsuppgift")]
        public bool Submission { get; set; }
        [Display(Name = "Startdatum")]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [Display(Name = "Slutdatum")]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);
        public int ModulId { get; set; }    
        public ModulModel Modul { get; set; }
        public ICollection<AssignmentModel> Assignments { get; set; }

    }
}
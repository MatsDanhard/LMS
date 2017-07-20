using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LmsTool.Models.DbModels;

namespace LmsTool.Models.Viewmodels
{
    public class ViewActivitys
    {
        public int Id { get; set; }
        [Display(Name="Aktivitetstyp")]
        public string TypeOfActivity { get; set; }
        [Display(Name = "Aktivitet")]
        public string Name { get; set; }
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }
        [Display(Name = "Modulens namn")]
        public string ModulName { get; set; }
        [Display(Name = "Modulens startdatum")]
        public string ModulStartStr { get; set; }
        [Display(Name = "Modulens slutdatum")]
        public string ModulEndStr { get; set; } 
        [Display(Name = "Starttid")]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [Display(Name = "Sluttid")]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);
        [HiddenInput]
        public int ModulId { get; set; }
        
        public List<AssignmentModel> Assignments { get; set; }
    }
}
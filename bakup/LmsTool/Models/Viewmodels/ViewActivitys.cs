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
        public string TypeOfActivity { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string ModulName { get; set; }
        public string ModulStartStr { get; set; }
        public string ModulEndStr { get; set; } 
        [Display(Name = "Startdatum")]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [Display(Name = "Slutdatum")]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);
        [HiddenInput]
        public int ModulId { get; set; }
        
        public List<AssignmentModel> Assignments { get; set; }
    }
}
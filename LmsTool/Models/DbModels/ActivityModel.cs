using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LmsTool.Models.DbModels
{
    public class ActivityModel
    {
        public int Id { get; set; }
        public string TypeOfActivity { get; set; }  
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Submission { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ModulId { get; set; }    
        public ModulModel Modul { get; set; }
        public ICollection<AssignmentModel> Assignments { get; set; }

    }
}
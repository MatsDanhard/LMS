using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LmsTool.Models.Viewmodels
{
    public class CreateActivity
    {
        public string Name { get; set; }
        public string ModulName { get; set; }
        public string TypeOfActivity { get; set; }
        public string Description { get; set; }
        public DateTime ActivityStart { get; set; }
        public DateTime ActivityEnd { get; set; }
        public DateTime ModulStart { get; set; }
        public DateTime ModulEnd { get; set; }  
        public string DisplayModulStart { get; set; }
        public string DisplayModulEnd { get; set; }
        public int ModulId { get; set; }    
        public bool Submission { get; set; }
    }
}
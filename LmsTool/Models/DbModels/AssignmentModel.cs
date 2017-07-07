using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LmsTool.Models.DbModels
{
    public class AssignmentModel
    {
        public int Id { get; set; }
        public bool Submitted { get; set; }
        public string Email { get; set; }
        public int ActivityId { get; set; } 
        public ActivityModel Activity { get; set; }
    }
}
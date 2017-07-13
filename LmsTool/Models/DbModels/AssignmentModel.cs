using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LmsTool.Models.DbModels
{
    public class AssignmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Submitted { get; set; }
        public DateTime Deadline { get; set; }
        public int ActivityId { get; set; }
        public int AssignmentId { get; set; }
        public virtual ActivityModel Activity { get; set; }
        public virtual AssignmentModel Assignment { get; set; }
    }
}
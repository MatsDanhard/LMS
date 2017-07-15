using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LmsTool.Models.DbModels;

namespace LmsTool.Models.Viewmodels
{
    public class ViewStudents
    {
        [Key]
        public string Id { get; set; } 
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<AssignmentModel> Assignments { get; set; }
    }
}
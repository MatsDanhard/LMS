using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LmsTool.Models.DbModels;
using System.ComponentModel;

namespace LmsTool.Models.Viewmodels
{
    public class ViewStudents
    {
        [Key]
        public string Id { get; set; }
        [Display(Name = "Förnamn och efternamn")]
        public string FullName { get; set; }
        [Display(Name = "E-postadress")]
        public string Email { get; set; }
        public List<AssignmentModel> Assignments { get; set; }
    }
}
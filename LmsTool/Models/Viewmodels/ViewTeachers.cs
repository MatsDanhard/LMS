using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LmsTool.Models.Viewmodels
{
    public class ViewTeachers
    {
        [Key]
        public string Id { get; set; }
        [Display(Name = "Förnamn och efternamn")]
        public string FullName { get; set; }
        [Display(Name = "E-postadress")]
        public string Email { get; set; }
    }
}
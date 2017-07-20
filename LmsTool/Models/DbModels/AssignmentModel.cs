using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LmsTool.Models.DbModels
{
    public class AssignmentModel
    {
        public int Id { get; set; }
        [Display(Name = "Namn")]
        [Required]
        [StringLength(50, MinimumLength = 3 ,ErrorMessage = "Namnet behöver vara minst tre tecken långt")]
        public string Name { get; set; }
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }
        [Display(Name = "Inlämnad")]
        public DateTime? Submitted { get; set; }
        [Display(Name = "Godkänd")]
        public bool Approved { get; set; }  
        [Display(Name = "Senaste inlämningsdatum")]
        public DateTime Deadline { get; set; }
        [Display(Name = "Dokument")]
        public string Document { get; set; }
        public int ActivityId { get; set; }
        [Display(Name = "ElevId")]
        public string UserId { get; set; }
        [Display(Name = "Elev")]
        public string StudentName { get; set; }
        public virtual ActivityModel Activity { get; set; }
        
    }
}
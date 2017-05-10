using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TasksManagement.Models
{
    public class Attachment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Path { get; set; }
        public DateTime? UploadTime { get; set; }
        public bool ViewFlag { get; set; }


        [ForeignKey("User")][Display(Name = "User")]
        public string User_key { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("Type")][Display(Name ="Type")]
        public int? Type_key { get; set; }
        public AttachmentType Type { get; set; }

        [ForeignKey("Task")][Display(Name ="Task")]
        public int Task_Key { get; set; }
        public AssignedTask Task { get; set; }
    }
}
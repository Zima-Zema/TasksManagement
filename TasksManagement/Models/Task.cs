using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TasksManagement.Models
{
    public class AssignedTask
    {
        public AssignedTask()
        {
            Attachments = new List<Attachment>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? DeadTime { get; set; }

        public bool ViewFlag { get; set; }

        [ForeignKey("Owner")][Display(Name ="Owner")]
        public string Owner_Key { get; set; }
        public virtual ApplicationUser Owner { get; set; }


        [ForeignKey("Creator")] [Display(Name ="Created By")]
        public string Creator_Key { get; set; }
        public virtual ApplicationUser Creator { get; set; }


        [ForeignKey("Status")][Display(Name ="Status")]
        public int Status_Key { get; set; }
        public Status Status { get; set; }

        public virtual List<Attachment> Attachments { get; set; }




    }
}
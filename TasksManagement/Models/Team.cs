using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TasksManagement.Models
{
    public class Team
    {
        public Team()
        {
            Members = new List<ApplicationUser>();
        }
        [Key]
        public int Id { get; set; }
        [StringLength(255)][Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool ViewFlag { get; set; }

        [ForeignKey("TeamLeader")][Display(Name ="Leader")]
        public string Leader_Key { get; set; }
        public virtual ApplicationUser TeamLeader { get; set; }

        [InverseProperty("Team")]
        public virtual List<ApplicationUser> Members { get; set; }


    }
}
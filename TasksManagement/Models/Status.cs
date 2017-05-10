using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TasksManagement.Models
{
    enum StatusLevel
    {
        New,
        InProgress,
        Closed
    }
    public class Status
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }

        public int Level { get; set; }
        public string Description { get; set; }
        public bool ViewFlag { get; set; }

    }
}
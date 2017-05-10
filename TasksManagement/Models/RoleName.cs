using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using TasksManagement.Models;

namespace TasksManagement.Models
{
    public class RoleName
    {
        public const string Admin = "Admin";
        public const string Member = "Member";
        public const string TeamLeader = "Team Leader";


       
    }
}
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TasksManagement.Models;

namespace TasksManagement.ViewModel
{
    public class UsersViewModel
    {
        public IEnumerable<IdentityRole> Roles { get; set; }
        public ApplicationUser User { get; set; }

        public IEnumerable<Team> Teams { get; set; }

        [Required]
        [Display(Name = "User Roles")]
        public string UserRoles { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
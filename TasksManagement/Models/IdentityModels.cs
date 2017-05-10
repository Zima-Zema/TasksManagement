using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace TasksManagement.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        [Display(Name ="First Name")][StringLength(255)][Required]
        public string FirstName { get; set; }
        [Display(Name = "Second Name")]
        [StringLength(255)][Required]
        public string SecondName { get; set; }
        [Display(Name ="Full Name")]
        [StringLength(255)]
        public string FullName { get; set; }
        public bool ViewFlag { get; set; }

        [ForeignKey("Team")] [Display(Name ="Team")]
        public int? Team_Key { get; set; }
        public virtual Team Team { get; set; }

        public virtual List<AssignedTask> Tasks { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }


        
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<AssignedTask> Tasks { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<AttachmentType> AttachmentTypes { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
    }


    
}
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;
using System.Linq;
using TasksManagement.Models;

[assembly: OwinStartupAttribute(typeof(TasksManagement.Startup))]
namespace TasksManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));



            // In Startup iam creating first Admin Role and creating a default Admin User 
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website				

                var user = new ApplicationUser();
                user.UserName = "Admin";
                user.Email = "Admin@Stingray.com";
                
                string userPWD = "Admin@159";

                var chkUser = UserManager.Create(user, userPWD);

              


                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, RoleName.Admin);

                }
               
                context.SaveChanges();
            }

            // creating Creating Manager role 
            if (!roleManager.RoleExists(RoleName.Member))
            {
                var role = new IdentityRole();
                role.Name = RoleName.Member;
                roleManager.Create(role);

            }

            // creating Creating Employee role 
            if (!roleManager.RoleExists(RoleName.TeamLeader))
            {
                var role = new IdentityRole();
                role.Name = RoleName.TeamLeader;
                roleManager.Create(role);

            }
        }

    }
}

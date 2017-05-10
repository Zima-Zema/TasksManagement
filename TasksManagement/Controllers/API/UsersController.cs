using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TasksManagement.Models;

namespace TasksManagement.Controllers.API
{
    public class UsersController : ApiController
    {
        private ApplicationDbContext _context;
        RoleManager<IdentityRole> roleManager;
        UserManager<ApplicationUser> UserManager;
        public UsersController()
        {
            _context = new ApplicationDbContext();
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

        }
        public IHttpActionResult GetCustomers()
        {
            var role = roleManager.FindByName(RoleName.Admin).Users.First();
            return Ok(_context.Users.Include(uu => uu.Team).Where(uu => uu.ViewFlag == true && !uu.Id.Contains(role.UserId)).ToList());
        }

        [HttpDelete]
        public IHttpActionResult DeleteCustomer(string id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userAccount = UserManager.FindById(id);

            if (userAccount == null)
            {
                return NotFound();
            }
            userAccount.ViewFlag = false;
            var result = UserManager.Update(userAccount);
            _context.SaveChanges();
            return Ok();
        }
    }
}

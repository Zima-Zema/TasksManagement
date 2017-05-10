using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TasksManagement.Models;
using TasksManagement.ViewModel;

namespace TasksManagement.Controllers
{
    public class UsersController : Controller
    {
        RoleManager<IdentityRole> roleManager;
        UserManager<ApplicationUser> UserManager;
        ApplicationDbContext _context;
        public UsersController()
        {
            _context = new ApplicationDbContext();
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            
        }
        
       

        // GET: Users
        public ActionResult Index()
        {
            // _context.Roles
            var role = roleManager.FindByName(RoleName.Admin).Users.First();
            
            return View(_context.Users.Include(uu => uu.Team).Where(uu=>uu.ViewFlag==true && !uu.Id.Contains(role.UserId)).ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = _context.Users.Include(c => c.Team).SingleOrDefault(cu => cu.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult New()
        {
            var viewModel = new UsersViewModel()
            {
                Roles = _context.Roles.Where(u => !u.Name.Contains(RoleName.Admin)).ToList(),
                Teams = _context.Teams.ToList(),
                User = new ApplicationUser()
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(UsersViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new UsersViewModel
                {
                    Roles = _context.Roles.Where(u => !u.Name.Contains("Admin")).ToList(),
                    Teams = _context.Teams.ToList(),
                    User = model.User
                };
                return View(viewModel);
            }
            var userAccount = UserManager.FindById(model.User.Id);
            if (userAccount==null)
            {
                var user = model.User;
                user.FullName = model.User.FirstName + " " + model.User.SecondName;
                user.ViewFlag = true;
                var chkUser = UserManager.Create(user, model.Password);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, model.UserRoles);

                }
            }
            else
            {
                
                

                userAccount.FirstName = model.User.FirstName;
                userAccount.SecondName = model.User.SecondName;
                userAccount.FullName = model.User.FirstName + " " + model.User.SecondName;

                userAccount.Email = model.User.Email;
                userAccount.UserName = model.User.UserName;
                userAccount.Team_Key = model.User.Team_Key;
                userAccount.Roles.Clear();
                var result1 = UserManager.AddToRole(userAccount.Id, model.UserRoles);

                //UserManager.RemoveFromRoles(userAccount)

                try
                {
                    var result =  UserManager.Update(userAccount);
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                             validationError.PropertyName,
                                             validationError.ErrorMessage);
                        }
                    }
                }



            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Users");
            
        }


        public ActionResult Edit(string id)
        {
            var userAccount = UserManager.FindById(id);
            //var roles = UserManager.GetRoles(id);
            if (userAccount == null)
            {
                return HttpNotFound();
            }
            var test = userAccount.Roles.FirstOrDefault(r => r.UserId == id).RoleId;
            var viewModel = new UsersViewModel
            {
                Roles = _context.Roles.Where(u => !u.Name.Contains("Admin")).ToList(),
                Teams = _context.Teams.ToList(),
                UserRoles = _context.Roles.SingleOrDefault(rr=>rr.Id==test).Name,
                User = userAccount
            };
            return View("New", viewModel);
        }

    }
}
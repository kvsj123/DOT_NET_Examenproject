using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DOT_NET_Examenproject.Data;
using DOT_NET_Examenproject.Models;
using DOT_NET_Examenproject.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace DOT_NET_Examenproject.Controllers
{
    public class UsersController : Controller
    {
      private readonly AppDbContext _db;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _user;

        public UsersController(AppDbContext db, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> user)
        {
            _db = db; 
            _user = user;
        }

        // GET: Users/Create
        public IActionResult Index()
        {
            var userList = _db.Users.ToList();
            var roleList = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach(var user in userList)
            {
                var role = roleList.FirstOrDefault(x => x.UserId == user.Id);
                if(role == null)
                {
                    user.Role = "None";
                }
                else
                {
                    user.Role = roles.FirstOrDefault(u => u.Id == role.RoleId).Name;
                }
            }
            return View(userList);
        }


        [HttpGet]
        public IActionResult Edit(string userId)
        {
            var user = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);
            if(user == null)
            {
                return NotFound();
            }


            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();
            var role = userRole.FirstOrDefault(u => u.UserId == user.Id);
            if(role != null)
            {
                user.RoleId = roles.FirstOrDefault(u => u.Id == role.RoleId).Id;
            }
            

            user.RoleList = _db.Roles.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id
            });
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            
            if (ModelState.IsValid)
            {
                var userDbValue = _db.ApplicationUser.FirstOrDefault(u => u.Id == user.Id);
                if(userDbValue == null)
                {
                    return NotFound();
                }
                var userRole = _db.UserRoles.FirstOrDefault(u => u.UserId == userDbValue.Id);
                if( userRole != null)
                {
                    var prevRoleName = _db.Roles.Where(u => u.Id == userRole.RoleId).Select(e => e.Name).FirstOrDefault();
                    await _user.RemoveFromRoleAsync(userDbValue, prevRoleName);
                }
                await _user.AddToRoleAsync(userDbValue, _db.Roles.FirstOrDefault(u => u.Id == user.RoleId).Name);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            user.RoleList = _db.Roles.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id
            });
            return View(user);
        }

        [HttpPost]
        public IActionResult Delete(string userId)
        {
            var user = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);
            if(user == null)
            {
                return NotFound();
            }
            _db.ApplicationUser.Remove(user);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        
    }

}

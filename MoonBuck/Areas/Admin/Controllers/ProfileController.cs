using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoonBuck.DataAccess.Data;
using MoonBuck.Models;
using Microsoft.AspNetCore.Authorization;
using MoonBuck.Utility;
using System.Data;
using MoonBuck.Areas.Admin.ViewModels;

namespace MoonBuck.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_SystemAdmin)]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<CustomRole> _roleManager;
        public ProfileController(ApplicationDbContext db, RoleManager<CustomRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProfileManagement(string roleId)
        {
            ProfileManagementVM ProfileVM = new ProfileManagementVM()
            {
                Role = _db.Roles.FirstOrDefault(u => u.Id == roleId)
            };
            return View(ProfileVM);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<CustomRole> objUserList = _db.Roles.ToList();

            return Json(new { data = objUserList });
        }

        [HttpPost]
        public IActionResult ProfileManagement(ProfileManagementVM userManagmentVM)
        {

            CustomRole applicationUser = _db.Roles.FirstOrDefault(u => u.Id == userManagmentVM.Role.Id);
            if (userManagmentVM.Role.Name != null)
            {
                applicationUser.Name = userManagmentVM.Role.Name;
            }
            if (userManagmentVM.Role.Description != null)
            {
                applicationUser.Description = userManagmentVM.Role.Description;
            }

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {

            var objFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Suspending/Unsuspending" });
            }

            if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            {
                //user is currently locked and we need to unlock them
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _db.SaveChanges();
            return Json(new { success = true, message = "Operation Successful" });
        }
        #endregion

    }
}

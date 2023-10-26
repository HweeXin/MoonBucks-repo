using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoonBuck.DataAccess.Data;
using MoonBuck.Models;
using MoonBuck.Models.ViewModels;
using MoonBuck.Utility;
using System.Data;

namespace MoonBuck.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_SystemAdmin)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public UserController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserManagement(string userId)
        {
            UserManagementVM UserVM = new UserManagementVM()
            {
                ApplicationUser = _db.ApplicationUsers.FirstOrDefault(u => u.Id == userId)
            };
            return View(UserVM);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<ApplicationUser> objUserList = _db.ApplicationUsers.ToList();

            var userRoles = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach (var user in objUserList)
            {
                var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
            }

            return Json(new { data = objUserList });
        }

        [HttpPost]
        public IActionResult UserManagement(UserManagementVM userManagmentVM)
        {

            ApplicationUser applicationUser = _db.ApplicationUsers.FirstOrDefault(u => u.Id == userManagmentVM.ApplicationUser.Id);
            applicationUser.Name = userManagmentVM.ApplicationUser.Name;
            applicationUser.Description = userManagmentVM.ApplicationUser.Description;
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

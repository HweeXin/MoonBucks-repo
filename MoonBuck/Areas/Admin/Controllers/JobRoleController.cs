using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoonBuck.DataAccess.Data;
using MoonBuck.DataAccess.Repository;
using MoonBuck.DataAccess.Repository.IRepository;
using MoonBuck.Models;
using MoonBuck.Utility;

namespace MoonBuck.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_SystemAdmin + ", " + SD.Role_CafeOwner +  ", " + SD.Role_CafeManager)]
    public class JobRoleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public JobRoleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<JobRole> roleList = _unitOfWork.Role.GetAll().ToList();
            return View(roleList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(JobRole obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Name and Display Order cannot be the same");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Role.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Role added successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            JobRole? roleFromDb = _unitOfWork.Role.Get(u => u.Id == id);
            //Role? roleFromDb1 = _db.Roles.FirstOrDefault(u => u.Id == id);
            //Role? roleFromDb2 = _db.Roles.Where(u => u.Id == id).FirstOrDefault();
            if (roleFromDb == null)
            {
                return NotFound();
            }
            return View(roleFromDb);
        }
        [HttpPost]
        public IActionResult Edit(JobRole obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Role.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Role updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            JobRole? roleFromDb = _unitOfWork.Role.Get(u => u.Id == id);
            if (roleFromDb == null)
            {
                return NotFound();
            }
            return View(roleFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            JobRole? obj = _unitOfWork.Role.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Role.Remove(obj);
            TempData["success"] = "Role deleted successfully";
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}

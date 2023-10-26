using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoonBuck.DataAccess.Repository.IRepository;
using MoonBuck.Models.ViewModels;
using MoonBuck.Models;
using System.Diagnostics;

namespace MoonBuck.Areas.Staff.Controllers
{
    [Area("Staff")]
    public class BidController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BidController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Bid> bidList = _unitOfWork.Bid.GetAll(includeProperties: "Slot").ToList();

            return View(bidList);
        }
        public IActionResult Upsert(int? id)
        {
            BidVM bidVM = new()
            {
                SlotList = _unitOfWork.Slot
                    .GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }),
                Bid = new Bid()
                {
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(1)
                }
            };
            if (id == null || id == 0)
            {
                //create
                return View(bidVM);
            }
            else
            {
                //update
                bidVM.Bid = _unitOfWork.Bid.Get(u => u.Id == id);
                return View(bidVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(BidVM obj)
        {
            if (obj.Bid.StartTime > obj.Bid.EndTime)
            {
                ModelState.AddModelError("StartTime", "The Start Time cannot be more than End Time");
            }
            if (ModelState.IsValid)
            {
                if (obj.Bid.Id == 0)
                {
                    _unitOfWork.Bid.Add(obj.Bid);
                }
                else
                {
                    _unitOfWork.Bid.Update(obj.Bid);
                }
                _unitOfWork.Save();
                TempData["success"] = "Slot added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                obj.SlotList = _unitOfWork.Slot
                    .GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    });
                return View(obj);
            }
        }

        #region API Call
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Bid> bidList = _unitOfWork.Bid.GetAll(includeProperties: "Slot").ToList();
            return Json(new { data = bidList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var slotToBeDeleted = _unitOfWork.Slot.Get(u => u.Id == id);
            if (slotToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Slot.Remove(slotToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "delete successful" });
        }
        #endregion
    }
}

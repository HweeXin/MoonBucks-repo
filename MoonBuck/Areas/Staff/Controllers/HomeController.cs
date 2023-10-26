using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoonBuck.DataAccess.Repository.IRepository;
using MoonBuck.Models;
using MoonBuck.Models.ViewModels;
using System.Diagnostics;

namespace MoonBuck.Areas.Staff.Controllers
{
    [Area("Staff")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Slot> slotList = _unitOfWork.Slot.GetAll(includeProperties: "Role").ToList();

            return View(slotList);
        }
        public IActionResult Details(int id)
        {
            SlotVM slotVM = new()
            {
                RoleList = _unitOfWork.Role
                    .GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }),
                Slot = new Slot()
                {
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(1)
                }
            };
            slotVM.Slot = _unitOfWork.Slot.Get(u => u.Id == id);
            return View(slotVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region API Call
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Slot> slotList = _unitOfWork.Slot.GetAll(includeProperties: "Role").ToList();
            return Json(new { data = slotList });
        }
        #endregion
    }
}
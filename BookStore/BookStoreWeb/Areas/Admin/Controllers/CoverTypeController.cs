using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICoverTypeRepository _coverTypeRepository;

        public CoverTypeController(IUnitOfWork context)
        {
            _unitOfWork = context;
            _coverTypeRepository = _unitOfWork.CoverType;
        }

        public IActionResult Index()
        {
            return View(_coverTypeRepository.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverType)
        {
            if (_coverTypeRepository.GetFirstOrDefault(c => c.Name == coverType.Name) != null)
            {
                ModelState.AddModelError("Name", "Deze kaftsoort bestaat al");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _coverTypeRepository.Add(coverType);
                    _unitOfWork.Save();
                    TempData["result"] = "Kaftsoort succesvol toegevoegd.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Er is een probleem met de database!";
                    return View(coverType);
                }
            }
            return View(coverType);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            CoverType coverType = _coverTypeRepository.GetFirstOrDefault(c => c.Id == id);
            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _coverTypeRepository.Update(coverType);
                    _unitOfWork.Save();
                    TempData["result"] = "Kaftsoort succesvol gewijzigd.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Er is een probleem met de database!";
                    return View(coverType);
                }
            }
            return View(coverType);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            CoverType coverType = _coverTypeRepository.GetFirstOrDefault(c => c.Id == id);
            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(CoverType coverType)
        {
            try
            {
                _coverTypeRepository.Remove(coverType);
                _unitOfWork.Save();
                TempData["result"] = "Kaftsoort succesvol verwijderd.";
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Er is een probleem met de database!";
                return View("Delete", coverType);
            }
            return RedirectToAction("Index");
        }
    }
}
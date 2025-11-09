using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(IUnitOfWork context)
        {
            _unitOfWork = context;
            _categoryRepository = _unitOfWork.Category;
        }
        public IActionResult Index()
        {
            return View(_categoryRepository.GetAll());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Naam en Volgnummer mogen niet hetzelfde zijn");
            }

            if (_categoryRepository.GetFirstOrDefault(c => c.Name == category.Name) != null)
            {
                ModelState.AddModelError("uniquename", "Deze categorienaam bestaat al");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _categoryRepository.Add(category);
                    _unitOfWork.Save();
                    TempData["result"] = "Categorie succesvol toegevoegd.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Er is een probleem met de database!";
                    return View(category);
                }
            }
            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category category = _categoryRepository.GetFirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Naam en Volgnummer mogen niet hetzelfde zijn");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _categoryRepository.Update(category);
                    _unitOfWork.Save();
                    TempData["result"] = "Categorie succesvol gewijzigd.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Er is een probleem met de database!";
                    return View(category);
                }
            }
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category category = _categoryRepository.GetFirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(Category category)
        {
            try
            {
                _categoryRepository.Remove(category);
                _unitOfWork.Save();
                TempData["result"] = "Categorie succesvol verwijderd.";
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Er is een probleem met de database!";
                return View("Delete", category);
            }
            return RedirectToAction("Index");
        }
    }
}
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _context;

        public CategoryController(ICategoryRepository context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.GetAll());
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

            if (_context.GetFirstOrDefault(c => c.Name == category.Name) != null)
            {
                ModelState.AddModelError("uniquename", "Deze categorienaam bestaat al");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(category);
                    _context.Save();
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
            Category category = _context.GetFirstOrDefault(c => c.Id == id);
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
                    _context.Update(category);
                    _context.Save();
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
            Category category = _context.GetFirstOrDefault(c => c.Id == id);
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
                _context.Remove(category);
                _context.Save();
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
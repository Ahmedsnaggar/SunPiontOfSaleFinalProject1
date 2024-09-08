using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunPiontOfSaleFinalProject.App.Models;
using SunPiontOfSaleFinalProject.Entiteis.Models;

namespace SunPiontOfSaleFinalProject.App.Controllers
{
    public class CategoriesController : Controller
    {
        private MyDbContext _db;

        public CategoriesController(MyDbContext db)
        {
            _db = db;
        }

        // GET: CategoriesController
        public ActionResult Index()
        {
            var categories = _db.Categories;
            return View("CategoriesList", categories);
        }

        // GET: CategoriesController/Details/5
        public ActionResult Details(int id)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            return View(category);
        }
        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            return View("NewCategory");
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category item)
        {
            try
            {
                var categoryTest = _db.Categories.Any(c=> c.CategoryName == item.CategoryName);
                if (categoryTest)
                {
                    ViewBag.ExistsError = "Category Name already exists";
                    return View("NewCategory");
                }
                _db.Categories.Add(item);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("NewCategory");
            }
        }

        // GET: CategoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            var category = _db.Categories.FirstOrDefault(c=> c.Id == id);
            if(category == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category Item)
        {
            try
            {
                var category = _db.Categories.FirstOrDefault(c=>c.Id == id);
                category.CategoryName = Item.CategoryName;
                category.CategoryDescription = Item.CategoryDescription;
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController/Delete/5
        public ActionResult Delete(int id)
        {
            var category = _db.Categories.FirstOrDefault(c=> c.Id == id);
            return View(category);
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var category = _db.Categories.FirstOrDefault(c => c.Id == id);
                _db.Categories.Remove(category);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

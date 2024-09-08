using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContextFile;
using SunPiontOfSaleFinalProject.Entiteis.Models;
using SunPiontOfSaleFinalProject.Repositories.Interfaces;

namespace SunPiontOfSaleFinalProject.App.Controllers
{
    public class CategoriesController : Controller
    {
        private ICategoreRepository _category;
        public CategoriesController(ICategoreRepository category)
        {
            _category = category;
        }

        // GET: CategoriesController
        public async Task<ActionResult> Index()
        {
            var categories = await _category.GetAllCategories();
            return View("CategoriesList", categories);
        }

        // GET: CategoriesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var category = await _category.GetCategoryById(id);
            return View(category);
        }
        // GET: CategoriesController/Create
        public async Task<ActionResult> Create()
        {
            return View("NewCategory");
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category item)
        {
            try
            {
                //var categoryTest = await _category.GetAllCategories().Result.
                //    (c=> c.CategoryName == item.CategoryName);
                //if (categoryTest)
                //{
                //    ViewBag.ExistsError = "Category Name already exists";
                //    return View("NewCategory");
                //}
               await  _category.AddCategory(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("NewCategory");
            }
        }

        // GET: CategoriesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var category = await _category.GetCategoryById(id);
            if(category == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Category Item)
        {
            try
            {
             await   _category.UpDateCategory(id, Item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var category = await _category.GetCategoryById(id);
            return View(category);
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _category.DeleteCategory(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

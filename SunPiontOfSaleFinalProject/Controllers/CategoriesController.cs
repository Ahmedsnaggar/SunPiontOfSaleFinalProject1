using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ContextFile;
using SunPiontOfSaleFinalProject.Entiteis.Models;
using SunPiontOfSaleFinalProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SunPiontOfSaleFinalProject.App.Controllers
{
    public class CategoriesController : Controller
    {
       private IBaseRepository<Category> _categoryRepository;
        public CategoriesController(IBaseRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [Authorize(Roles = "Admin, User")]
        // GET: CategoriesController
        public async Task<ActionResult> Index()
        {
            var categories = await _categoryRepository.GetAll();
            return View("CategoriesList", categories);
        }
        [Authorize(Roles = "Admin")]
        // GET: CategoriesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var category = await _categoryRepository.GetById(id);
            return View(category);
        }
        [Authorize(Roles = "Admin")]
        // GET: CategoriesController/Create
        public async Task<ActionResult> Create()
        {
            return View("NewCategory");
        }
        [Authorize(Roles = "Admin")]
        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category item)
        {
            try
            {
                var categoryTest =  _categoryRepository.GetAll().Result.Any
                    (c => c.CategoryName == item.CategoryName);
                if (categoryTest)
                {
                    ViewBag.ExistsError = "Category Name already exists";
                    return View("NewCategory");
                }
                await  _categoryRepository.AddItem(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("NewCategory");
            }
        }
        [Authorize(Roles = "Admin")]
        // GET: CategoriesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetById(id);
            if(category == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        [Authorize(Roles = "Admin")]
        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Category Item)
        {
            try
            {
             await   _categoryRepository.UpdateItem(Item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin")]
        // GET: CategoriesController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetById(id);
            return View(category);
        }
        [Authorize(Roles = "Admin")]
        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _categoryRepository.DeleteItem(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
